﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using K9Abp.Core.Authorization.Users;

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
        public virtual long CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual DeskworkCustomer Customer { get; set; }

        [Required]
        public EWorkCompletion Completion { get; set; }

        public virtual IList<DeskworkStep> Steps { get; set; }

        public virtual IList<DeskworkFollower> Followers { get; set; }

        #endregion

        #region Constructor

        public Deskwork()
        {
            Steps = new List<DeskworkStep>();
            Followers = new List<DeskworkFollower>();
        }

        #endregion

        #region AggregateRoot Methods

        public void AddFollower(User follower)
        {
            if (Followers.Any(x => x.FollowerId == follower.Id)) return;

            Followers.Add(new DeskworkFollower
            {
                FollowerId = follower.Id,
                WorkId = Id
            });
        }

        public void RemoveFollower(User follower)
        {
            var exist = Followers.FirstOrDefault(x => x.FollowerId == follower.Id);
            if(exist != null)
            {
                Followers.Remove(exist);
            }
        }        

        #endregion
    }
}