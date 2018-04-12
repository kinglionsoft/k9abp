using System.IO;
using System.Text;
using DotLiquid;

namespace K9Abp.AngularClientGenerator
{
    public static class DotLiquidExtensions
    {
        public static Template Load(string file)
        {
            using (var input = File.OpenText(file))
            {
                return Template.Parse(input.ReadToEnd());
            }
        }

        public static void RenderToFile(this object localVariables, string templateFile, string outputFile)
        {
            var template = Load(templateFile);
            var code = template.Render(Hash.FromAnonymousObject(new { Model = localVariables }));
            using (var writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                writer.Write(code);
            }
        }
    }
}


