using System.Globalization;
using System.Linq;
using Abp.Application.Features;
using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Web.Configuration;
using Abp.Web.Models.AbpUserConfiguration;
using Abp.Web.Security.AntiForgery;

namespace K9Abp.NgAlain.Configuration
{
    /// <summary>
    /// 修改AbpUserConfigurationBuilder，为abp-alain提供配置信息
    /// </summary>
    /// <remarks>
    /// 1. 支持i18n后，不再输出语言资源文件
    /// </remarks>
    public class K9AbpUserConfigurationBuilder : AbpUserConfigurationBuilder
    {
        public K9AbpUserConfigurationBuilder(IMultiTenancyConfig multiTenancyConfig, ILanguageManager languageManager, ILocalizationManager localizationManager, IFeatureManager featureManager, IFeatureChecker featureChecker, IPermissionManager permissionManager, IUserNavigationManager userNavigationManager, ISettingDefinitionManager settingDefinitionManager, ISettingManager settingManager, IAbpAntiForgeryConfiguration abpAntiForgeryConfiguration, IAbpSession abpSession, IPermissionChecker permissionChecker) : base(multiTenancyConfig, languageManager, localizationManager, featureManager, featureChecker, permissionManager, userNavigationManager, settingDefinitionManager, settingManager, abpAntiForgeryConfiguration, abpSession, permissionChecker)
        {
        }

        protected override AbpUserLocalizationConfigDto GetUserLocalizationConfig()
        {
            var currentCulture = CultureInfo.CurrentUICulture;
            var languages = LanguageManager.GetLanguages();

            var config = new AbpUserLocalizationConfigDto
            {
                CurrentCulture = new AbpUserCurrentCultureConfigDto
                {
                    Name = currentCulture.Name,
                    DisplayName = currentCulture.DisplayName
                },
                Languages = languages.ToList()
            };

            if (languages.Count > 0)
            {
                config.CurrentLanguage = LanguageManager.CurrentLanguage;
            }
            /*
           // 支持i18n后，不需要再输出语言资源
           //
           var sources = _localizationManager.GetAllSources().OrderBy(s => s.Name).ToArray();
           config.Sources = sources.Select(s => new AbpLocalizationSourceDto
           {
               Name = s.Name,
               Type = s.GetType().Name
           }).ToList();

           config.Values = new Dictionary<string, Dictionary<string, string>>();
           foreach (var source in sources)
           {
               var stringValues = source.GetAllStrings(currentCulture).OrderBy(s => s.Name).ToList();
               var stringDictionary = stringValues
                   .ToDictionary(_ => _.Name, _ => _.Value);
               config.Values.Add(source.Name, stringDictionary);
           }
           //*/
            return config;
        }
    }
}
