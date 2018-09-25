using Microsoft.AspNetCore.Http;

namespace K9Abp.Core.Extensions
{
    public static class RequestExtensions
    {
        public static string GetAbsoluteUrl(this HttpRequest request, string relativeUrl)
        {
            return $"{request.Scheme}://{request.Host}/{relativeUrl}";
        }
    }
}