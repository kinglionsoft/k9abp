using System.Linq;

namespace NSwag.CodeGeneration.TypeScript.Models
{
    public static class TypeScriptOperationModelExtensions
    {
        public static bool HasSecurity(this TypeScriptOperationModel model)
        {
            return model.Responses.Any(r => r.StatusCode == "401" || r.StatusCode == "403");
        }
    }
}


