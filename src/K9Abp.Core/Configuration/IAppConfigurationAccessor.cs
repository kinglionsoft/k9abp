using Microsoft.Extensions.Configuration;

namespace K9Abp.Core.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}

