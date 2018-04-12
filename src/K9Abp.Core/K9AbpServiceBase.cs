using Abp;

namespace K9Abp.Core
{
    /// <summary>
    /// This class can be used as a base class for services in this application.
    /// It has some useful objects property-injected and has some basic methods most of services may need to.
    /// It's suitable for non domain nor application service classes.
    /// For domain services inherit <see cref="K9AbpDomainServiceBase"/>.
    /// For application services inherit K9AbpAppServiceBase.
    /// </summary>
    public abstract class K9AbpServiceBase : AbpServiceBase
    {
        protected K9AbpServiceBase()
        {
            LocalizationSourceName = K9AbpConsts.LocalizationSourceName;
        }

        // TODO: Add your common members for all your services. 
    }
}
