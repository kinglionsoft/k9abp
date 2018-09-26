using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace K9Abp.Core.Extensions
{
    public static class RequestExtensions
    {
        public static string GetAbsoluteUrl(this HttpRequest request, string relativeUrl)
        {
            return $"{request.Scheme}://{request.Host}/{relativeUrl}";
        }

        public static bool IsWechat(this HttpRequest request)
        {
            var ua = request.Headers[HeaderNames.UserAgent].ToString();
            return ua.Contains("MicroMessenger");
        }
    }
}