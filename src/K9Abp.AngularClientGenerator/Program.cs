using System;
using System.IO;
using System.Linq;
using System.Text;
using DotLiquid;
using Newtonsoft.Json;
using NJsonSchema.CodeGeneration.TypeScript;
using NSwag;
using NSwag.CodeGeneration.OperationNameGenerators;
using NSwag.CodeGeneration.TypeScript;

namespace K9Abp.AngularClientGenerator
{
    class Program
    {
        private const string DefaultSwaggerSpecUrl = "http://localhost:5000/swagger/v1/swagger.json";
        private const string DefaultOutputPath = @"..\..\..\..\..\..\abp-alain\src\app\abp\services";
        private const string ClientFile = "clients.ts";
        private const string ClassNameSuffix = "Client";
        private const string ModuleFile = "abp.service.module.ts";

        static void Main(string[] args)
        {
            var swaggerSpecUrl = args.Length > 0 ? args[0] : DefaultSwaggerSpecUrl;
            var outputPath = args.Length > 1 ? args[1] : DefaultOutputPath;
            Console.WriteLine($"Swagger描述文档地址为：{swaggerSpecUrl}");
            Console.WriteLine($"客户端代理类输出地址为：{Path.GetFullPath(outputPath)}");

            // 配置
            var document = SwaggerDocument.FromUrlAsync(swaggerSpecUrl).GetAwaiter().GetResult();
            var settings = new SwaggerToTypeScriptClientGeneratorSettings
            {
                ClassName = "{controller}" + ClassNameSuffix,
                OperationNameGenerator = new MultipleClientsFromPathSegmentsOperationNameGenerator(),
                HttpClass = HttpClass.HttpClient,
                InjectionTokenType = InjectionTokenType.InjectionToken,
                ClientBaseClass = null,
                Template = TypeScriptTemplate.Angular
            };

            settings.TypeScriptGeneratorSettings.DateTimeType = TypeScriptDateTimeType.Date;
            settings.TypeScriptGeneratorSettings.GenerateCloneMethod = false;
            settings.TypeScriptGeneratorSettings.GenerateConstructorInterface = false;
            settings.TypeScriptGeneratorSettings.ExtendedClasses = null;
            settings.CodeGeneratorSettings.TemplateDirectory = "Template";

            // 生成客户端代理类
            GenerateFile(document, settings, Path.Combine(outputPath, ClientFile));
            Console.WriteLine("生成客户端代理类成功");

            // 生成Module
            GenerateModule(document, settings, Path.Combine(outputPath, ModuleFile));
            Console.WriteLine("生成模块文件成功");

            // 生成index.ts
            GenerateIndex(Path.Combine(outputPath, "index.ts"));
            Console.WriteLine("生成index文件成功");
        }

        static void GenerateFile(SwaggerDocument document, SwaggerToTypeScriptClientGeneratorSettings settings, string clientFile)
        {
            var generator = new SwaggerToTypeScriptClientGenerator(document, settings);
            var code = generator.GenerateFile();
            using (var writer = new StreamWriter(clientFile, false, Encoding.UTF8))
            {
                writer.Write(code);
            }
        }

        static void GenerateModule(SwaggerDocument document, SwaggerToTypeScriptClientGeneratorSettings settings, string moduleFile)
        {
            // 生成Module
            var clients = document.Operations
                .Select(operation => settings.OperationNameGenerator.GetClientName(document, operation.Path, operation.Method, operation.Operation))
                .Distinct()
                .Select(c => $"{c}{ClassNameSuffix}");

            clients.RenderToFile(Path.Combine("Template", "Module.liquid"), moduleFile);
        }

        static void GenerateIndex(string indexFile)
        {
            DotLiquidExtensions.RenderFile(Path.Combine("Template", "Index.liquid"), indexFile);
        }
    }
}



