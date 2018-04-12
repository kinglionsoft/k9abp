using Abp.Domain.Services;

namespace K9Abp.Core
{
    public abstract class K9AbpDomainServiceBase : DomainService
    {
        // TODO: Add your common members for all your domain services. 

        protected K9AbpDomainServiceBase()
        {
            LocalizationSourceName = K9AbpConsts.LocalizationSourceName;
        }
    }
}
