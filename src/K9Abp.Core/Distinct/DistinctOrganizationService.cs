using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using K9Abp.Core;
using Microsoft.EntityFrameworkCore;

namespace Abp.Organizations
{
    public class DistinctOrganizationService: K9AbpDomainServiceBase, IDistinctOrganizationService
    {
        private readonly IRepository<County> _countyRepository;
        private readonly IRepository<Distinct> _distinctRepository;
        private readonly IRepository<DistinctOrganizationUnit, long> _distinctOrganizationRepository;

        public DistinctOrganizationService(IRepository<County> countyRepository, IRepository<Distinct> distinctRepository, IRepository<DistinctOrganizationUnit, long> distinctOrganizationRepository)
        {
            _countyRepository = countyRepository;
            _distinctRepository = distinctRepository;
            _distinctOrganizationRepository = distinctOrganizationRepository;
        }

        public IQueryable<DistinctOrganizationUnit>  Queryable()
        {
            return _distinctOrganizationRepository.GetAllIncluding(
                    x => x.Distinct, x => x.OrganizationUnit, x => x.Distinct.County)
                .AsNoTracking();
        }


        public async Task<DistinctOrganizationUnit> GetAsync(long organizationId)
        {
            return await _distinctOrganizationRepository.GetAllIncluding(
                    x => x.Distinct, x => x.OrganizationUnit, x => x.Distinct.County)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrganizationUnitId == organizationId);
        }
    }
}