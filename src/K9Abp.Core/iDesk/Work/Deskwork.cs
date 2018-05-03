using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;

namespace K9Abp.iDeskCore.Work
{
    [Audited]
    public class Deskwork : AuditedAggregateRoot<long>, IPassivable
    {
        #region IPassivable
        [Required]
        public virtual bool IsActive { get; set; }
        #endregion

        #region Properties

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public virtual EWorkUrgency Urgency { get; set; }

        [Required]
        public virtual EWorkStatus Status { get; set; }

        public long? RelatedWorkId { get; set; }

        [Required]
        public int TagId { get; set; }

        [StringLength(50)]
        public string TagName { get; set; } // Redundant

        [Required]
        public virtual long CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual DeskworkCustomer Customer { get; set; }

        /// <summary>
        /// 时限，单位：小时
        /// </summary>
        [Required]
        public int TimeLimit { get; set; }

        [Required]
        public EWorkCompletion Completion { get; set; }

        public DateTime? CompletionTime { get; set; }

        public virtual IList<DeskworkStep> Steps { get; set; }

        public virtual IList<DeskworkFollower> Followers { get; set; }

        #endregion

        #region Constructor

        public Deskwork()
        {
            Steps = new List<DeskworkStep>();
            Followers = new List<DeskworkFollower>();
            Status = EWorkStatus.未处理;
        }

        #endregion

        #region Follower

        public Deskwork Follow(long followerId)
        {
            if (Followers.Any(x => x.FollowerId == followerId)) return this;

            Followers.Add(new DeskworkFollower
            {
                FollowerId = followerId,
                WorkId = Id
            });

            DomainEvents.Add(new FollowEventData(followerId));
            return this;
        }

        public Deskwork Unfollow(long followerId)
        {
            var exist = Followers.FirstOrDefault(x => x.FollowerId == followerId);
            if (exist != null)
            {
                Followers.Remove(exist);
                DomainEvents.Add(new UnfollowEventData(followerId));
            }
            return this;
        }

        #endregion

        #region Customer

        public Deskwork SetCustomer(DeskworkCustomer customer)
        {
            if (CustomerId != customer.Id)
            {
                CustomerId = customer.Id;
                Customer = Customer;
            }
            return this;
        }

        #endregion

        #region Tag

        public Deskwork SetTagAsync(int tagId, string tagName)
        {
            if (TagId != tagId)
            {
                TagId = tagId;
                TagName = tagName;
            }
            return this;
        }

        #endregion

        #region Step

        private void EnsureActive()
        {
            if (!IsActive)
            {
                throw new UserFriendlyException($"工单({Id})已关闭");
            }
        }

        public Deskwork Complete()
        {
            EnsureActive();

            if (Completion != EWorkCompletion.未完成) return this;

            if (Steps.Any(x => !x.Done))
            {
                throw new UserFriendlyException("请先完成所有子流程");
            }

            CompletionTime = Clock.Now;

            if (CreationTime.AddHours(TimeLimit) <= CompletionTime)
            {
                Completion = EWorkCompletion.按时完成;
            }
            else
            {
                Completion = EWorkCompletion.超时完成;
            }

            foreach (var step in Steps)
            {
                step.Completion = Completion; // 工单流程的完成情况与工单一致
            }
            
            DomainEvents.Add(new WorkCompletionEventData());
            return this;
        }

        public Deskwork Close()
        {
            if (IsActive)
            {
                IsActive = false;
                DomainEvents.Add(new WorkCloseEventData());
            }
            return this;
        }

        public Deskwork CompleteStep(long currentUserId, long currentStepId, string result, DeskworkStep next)
        {
            EnsureActive();

            var currentStep = Steps.Single(x => x.Id == currentStepId);
            if (!currentStep.Done)
            {
                throw new UserFriendlyException("当前流程已结束");
            }
            if (currentStep.ReceiverId != currentUserId)
            {
                throw new UserFriendlyException("不能完成他人的流程");
            }
            currentStep.Complete(result);

            if (next == null)
            {
                // 没有转交给他人，直接完成当前工单
                Complete();
            }
            else
            {
                CreateStep(next);
            }

            return this;
        }

        public Deskwork CreateStep(DeskworkStep step)
        {
            EnsureActive();
            if (Steps.Count > 0)
            {
                var last = Steps.OrderBy(x => x.Id).Last();
                if (!last.Done)
                {
                    throw new UserFriendlyException("请首先完成上一个流程");
                }
            }

            Steps.Add(step);
            DomainEvents.Add(new StepChangeEventData());
            return this;
        }

        #endregion


    }
}