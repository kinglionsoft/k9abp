using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace K9Abp.Core.I18N
{
    internal static class K9AbpI18NConfigurer
    {
        internal static void Configure(string contentRootPath)
        {
            var langRootPath = Path.Combine(contentRootPath, "lang");
            if (!Directory.Exists(langRootPath))
            {
                throw new DirectoryNotFoundException(langRootPath);
            }
            var i18NRootPath = Path.Combine(contentRootPath, "wwwroot", "i18n");
            if (Directory.Exists(i18NRootPath))
            {
                Directory.Delete(i18NRootPath, true);
            }
            Directory.CreateDirectory(i18NRootPath);

            var abpSource = new Dictionary<string, IEnumerable<KeyValuePair<string, string>>>
            {
                {
                    K9AbpConsts.DefaultLanguae,
                    GetLangFiles(langRootPath)
                },
                {
                    "Abp",
                    GetLangFiles(Path.Combine(langRootPath, "Abp"))
                },
                {
                    "AbpWeb",
                    GetLangFiles(Path.Combine(langRootPath, "AbpWeb"))
                }
                ,
                {
                    "AbpZero",
                    GetLangFiles(Path.Combine(langRootPath, "AbpZero"))
                }
            };
            foreach (var lang in abpSource[K9AbpConsts.DefaultLanguae].Select(x => x.Key))
            {
                var abpLangs = abpSource.SelectMany(x => x.Value)
                    .Where(x => x.Key == lang)
                    .Select(x => x.Value)
                    .ToList();
                JsonHelper.MergeJsonFile("texts", Path.Combine(i18NRootPath, $"{lang}.json"), abpLangs);
            }

        }

        private static IEnumerable<KeyValuePair<string, string>> GetLangFiles(string path)
        {
            return Directory.GetFiles(path, "*.json").Select(x =>
            {
                var separatorCharIndex = x.LastIndexOf(Path.DirectorySeparatorChar) + 1;
                var fileName = x.Substring(separatorCharIndex, x.Length - 5 - separatorCharIndex);
                var fileWithLang = fileName.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                var lang = fileWithLang.Length > 1 ? fileWithLang.Last() : K9AbpConsts.DefaultLanguae;
                return new KeyValuePair<string, string>(lang, x);
            });
        }
    }
}
