using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using K9Abp.Application;
using K9Abp.Core.Authorization.Users;
using K9Abp.iDesk.Work.Dto;
using K9Abp.iDeskCore.Work;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Utilities;

namespace K9Abp.iDesk.Work
{
    public class WorkAppService : K9AbpAppServiceBase, IWorkAppService
    {
        private readonly IRepository<Deskwork, long> _workRepository;
        public IUserCache UserCache { get; set; }

        public WorkAppService(IRepository<Deskwork, long> workRepository)
        {
            _workRepository = workRepository;
            UserCache = NullUserCache.Instance;
        }

        #region Follower

        /// <summary>
        /// Follow the work
        /// </summary>
        /// <param name="workId"></param>
        /// <param name="followerId">if null, means the logined user</param>
        /// <returns></returns>
        public async Task<bool> Follow(long workId, long? followerId)
        {
            var work = await _workRepository.GetAllIncluding(x => x.Followers)
                .FirstOrDefaultAsync(x => x.Id == workId);
            if (work == null) return false;
            work.Follow(followerId ?? AbpSession.UserId.Value);
            return true;
        }

        /// <summary>
        /// Unfollow the work
        /// </summary>
        /// <param name="workId">Id of the work</param>
        /// <param name="followerId">Id of the follower. if null, means the logined user</param>
        public async Task<bool> Unfollow(long workId, long? followerId)
        {
            var work = await _workRepository.GetAllIncluding(x => x.Followers)
                .FirstOrDefaultAsync(x => x.Id == workId);
            if (work == null) return false;
            work.Unfollow(followerId ?? AbpSession.UserId.Value);
            return true;
        }

        #endregion

        #region Creation

        public async Task<long> Create(WorkCreateInput input)
        {
            var work = input.MapTo<Deskwork>();
            var receiver = UserCache.Get(input.ReceiverId);
            work.CreateStep(new DeskworkStep(AbpSession.UserId.Value, AbpSession.GetCurrentUser().Name, input.ReceiverId, receiver.Name, receiver.OrganizationUnitName));
            return await _workRepository.InsertAndGetIdAsync(work);
        }

        #endregion

        #region Step

        public async Task<bool> FinishStep(FinishStepInput input)
        {
            var work = await _workRepository.GetAllIncluding(x => x.Steps)
                .FirstOrDefaultAsync(x => x.Id == input.WorkId);
            EnsureWorkActive(work);

            DeskworkStep next = null;
            if (input.ReceiverId != null)
            {
                var receiver = await UserCache.GetAsync(input.ReceiverId.Value);

                next = new DeskworkStep(AbpSession.UserId.Value,
                    AbpSession.GetCurrentUser().Name,
                    receiver.Id,
                    receiver.Name,
                    receiver.OrganizationUnitName
                );
            }

            work.CompleteStep(AbpSession.UserId.Value, input.StepId, input.Result, next);
            return true;
        }

        #endregion

        private void EnsureWorkActive(Deskwork work)
        {
            if (work == null)
            {
                throw new UserFriendlyException("工单不存在");
            }

            if (!work.IsActive || work.Completion != EWorkCompletion.未完成)
            {
                throw new UserFriendlyException($"工单({work.Id})已完成或者已关闭");
            }
        }
    }
}