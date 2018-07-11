using System.Threading.Tasks;
using Abp.Dependency;

namespace Abp.Organizations
{
    public interface IDistinctOrganizationService: ITransientDependency
    {
        System.Linq.IQueryable<DistinctOrganizationUnit> Queryable();
        Task<DistinctOrganizationUnit> GetAsync(long organizationId);
    }
}