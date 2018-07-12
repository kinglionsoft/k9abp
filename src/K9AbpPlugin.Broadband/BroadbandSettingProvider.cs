using System.Collections.Generic;
using Abp.Configuration;

namespace K9AbpPlugin.Broadband
{
    public class BroadbandSettingProvider: SettingProvider
    {
        public const string WarnThreshold = "Plg.Broadband.WarnThreshold";

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                    WarnThreshold,
                    "30",
                    scopes: SettingScopes.Application | SettingScopes.Tenant
                )
            };
        }
    }
}