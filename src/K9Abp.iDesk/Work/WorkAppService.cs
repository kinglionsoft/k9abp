using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using K9Abp.Application;
using K9Abp.iDesk.Work.Dto;
using K9Abp.iDeskCore.Work;
using Microsoft.EntityFrameworkCore;

namespace K9Abp.iDesk.Work
{
    public class WorkAppService : K9AbpAppServiceBase, IWorkAppService
    {
        private readonly IRepository<Deskwork, long> _workRepository;

        public WorkAppService(IRepository<Deskwork, long> workRepository)
        {
            _workRepository = workRepository;
        }

        #region Follower

        public async Task<bool> Follow(long workId, long followerId)
        {
            var work = await _workRepository.GetAllIncluding(x => x.Followers)
                .FirstOrDefaultAsync(x => x.Id == workId);
            if (work == null) return false;
            work.Follow(followerId);
            return true;
        }

        #endregion

        #region Creation

        public async Task<long> Create(WorkCreateInput input)
        {
            var work = input.MapTo<Deskwork>();
            work.CreateStep(currentUserId, currentUserName, input.ReceiverId, receiverName);
            return await _workRepository.InsertAndGetIdAsync(work);
        }

        #endregion
    }
}