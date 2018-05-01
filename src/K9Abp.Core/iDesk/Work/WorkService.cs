using Abp.Domain.Repositories;
using K9Abp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;

namespace K9Abp.iDeskCore.Work
{
    public class WorkService: K9AbpDomainServiceBase
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
            var custom = _customerRepository.GetAsync(customerId);
            if (custom == null)
            {
                throw new UserFriendlyException(-404, L("NotFound"));
            }

            var work = await _workRepository.GetAsync(workId);
            work.CustomerId = customerId;
        }

        #endregion
    }
}
