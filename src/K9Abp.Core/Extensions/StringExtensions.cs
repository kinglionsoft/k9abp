using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Abp.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string camelCase)
        {
            var sb = new StringBuilder();
            sb.Append(char.ToLower(camelCase[0]));
            for (var i = 1; i < camelCase.Length; i++)
            {
                if (char.IsUpper(camelCase[i]))
                {
                    sb.Append("_" + char.ToLower(camelCase[i]));
                }
                else
                {
                    sb.Append(camelCase[i]);
                }
            }
            return sb.ToString();
        }

        public static string EnsureNotEndWith(this string raw, char c, bool ignoreCase = true)
        {
            if (raw[raw.Length - 1] == c || ignoreCase && raw[raw.Length - 1] == char.ToUpper(c))
            {
                return raw.Remove(raw.Length - 1);
            }
            return raw;
        }

        /// <summary>
        /// 单词变成单数形式
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string ToSingular(this string word)
        {
            var regs = new[]
            {
                new KeyValuePair<string, string>("${keep}y", "(?<keep>[^aeiou])ies$"),
                new KeyValuePair<string, string>("${keep}", "(?<keep>[aeiou]y)s$"),
                new KeyValuePair<string, string>("${keep}", "(?<keep>[sxzh])es$"),
                new KeyValuePair<string, string>("${keep}", "(?<keep>[^sxzhyu])s$")
            };

            foreach (var r in regs)
            {
                var regex = new Regex(r.Value);
                if (regex.IsMatch(word))
                {
                    return regex.Replace(word, r.Key);
                }
            }

            return word;
        }
        /// <summary>
        /// 单词变成复数形式
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string ToPlural(this string word)
        {
            var regs = new[]
            {
                new KeyValuePair<string, string>("${keep}ies", "(?<keep>[^aeiou])y$"),
                new KeyValuePair<string, string>("${keep}s", "(?<keep>[aeiou]y)$"),
                new KeyValuePair<string, string>("${keep}es", "(?<keep>[sxzh])$"),
                new KeyValuePair<string, string>("${keep}s", "(?<keep>[^sxzhy])$")
            };

            foreach (var r in regs)
            {
                var regex = new Regex(r.Value);
                if (regex.IsMatch(word))
                {
                    return regex.Replace(word, r.Key);
                }
            }

            return word;
        }
    }
}