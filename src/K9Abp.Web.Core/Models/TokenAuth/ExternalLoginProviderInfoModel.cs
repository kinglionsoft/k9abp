using Abp.AutoMapper;
using K9Abp.Web.Core.Authentication.External;

namespace K9Abp.Web.Core.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}

