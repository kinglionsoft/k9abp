using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Abp.Application.Services;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace K9Abp.Wechat.Services
{
    internal interface IWechatService: IApplicationService
    {
        List<WechatSettingOutput> GetWechatSettings();
    }
}