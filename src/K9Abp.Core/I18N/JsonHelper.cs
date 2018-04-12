using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace K9Abp.Core.I18N
{
    internal static class JsonHelper
    {
        internal static void MergeJsonFile(string tokenPath, string destFile, IEnumerable<string> sourceFiles)
        {
            JObject dest = new JObject();
            foreach (var file in sourceFiles)
            {
                var sourceJson = ReadFromFile(file);
                var token = sourceJson.SelectToken(tokenPath);
                dest.Merge(token);
            }
            using (var writer = new StreamWriter(destFile, false, Encoding.UTF8))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(jsonWriter, dest);
            }
        }

        private static JObject ReadFromFile(string file)
        {
            using (var reader = new StreamReader(file))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return JObject.Load(jsonReader);
            }
        }
    }
}

