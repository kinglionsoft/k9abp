using Abp.Domain.Repositories;
using K9Abp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.UI;

namespace K9Abp.iDeskCore.Work
{
    public class WorkService : K9AbpDomainServiceBase
    {
        private readonly IRepository<DeskworkCustomer, long> _customerRepository;
        private readonly IRepository<DeskworkFollower, long> _followerRepository;
        private readonly IRepository<DeskworkStep, long> _stepRepository;
        private readonly IRepository<DeskworkTag> _tagRepository;
        private readonly IRepository<Deskwork, long> _workRepository;

        public WorkService(IRepository<DeskworkCustomer, long> customerRepository,
            IRepository<DeskworkFollower, long> followerRepository,
            IRepository<DeskworkStep, long> stepRepository,
            IRepository<DeskworkTag> tagRepository,
            IRepository<Deskwork, long> workRepository)
        {
            _customerRepository = customerRepository;
            _followerRepository = followerRepository;
            _stepRepository = stepRepository;
            _tagRepository = tagRepository;
            _workRepository = workRepository;
        }


        #region Customer

        public async Task SetCustomerAsync(long workId, long customerId)
        {
            var customer = _customerRepository.GetAsync(customerId); // throw EntityNotFoundException if not found
            var work = await _workRepository.GetAsync(workId);
            work.CustomerId = customer.Id;
        }

        #endregion

        #region Follower

        public async Task FollowAsync(long workId, long followerId)
        {
            var exist = await _followerRepository.FirstOrDefaultWithoutTrackingAsync(x => x.WorkId == workId && x.FollowerId == followerId);
            if (exist == null)
            {
                await _followerRepository.InsertAsync(new DeskworkFollower
                {
                    WorkId = workId,
                    FollowerId = followerId
                });
            }
        }

        public async Task UnfollowAsync(long workId, long followerId)
        {
            var exist = await _followerRepository.FirstOrDefaultWithoutTrackingAsync(x => x.WorkId == workId && x.FollowerId == followerId);
            if (exist != null)
            {
                await _followerRepository.DeleteAsync(exist.Id);
            }
        }

        #endregion

        #region Tag

        public async Task ChangeTagAsync(long workId, int tagId)
        {
            var work = await _workRepository.GetAsync(workId);
            if (work.TagId != tagId)
            {
                var tag = await _tagRepository.GetAsync(tagId);
                work.TagId = tagId;
                work.TagName = tag.Name;
            }
        }

        #endregion

        #region Work


        #endregion
    }
}
