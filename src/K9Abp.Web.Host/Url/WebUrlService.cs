using Abp.Dependency;
using K9Abp.Core.Configuration;
using K9Abp.Core.Url;
using K9Abp.Web.Core.Url;

namespace MyCompanyName.AbpZeroTemplate.Web.Url
{
    public class WebUrlService : WebUrlServiceBase, IWebUrlService, ITransientDependency
    {
        public WebUrlService(
            IAppConfigurationAccessor configurationAccessor) :
            base(configurationAccessor)
        {
        }

        public override string WebSiteRootAddressFormatKey => "App:ClientRootAddress";

        public override string ServerRootAddressFormatKey => "App:ServerRootAddress";
    }
}
