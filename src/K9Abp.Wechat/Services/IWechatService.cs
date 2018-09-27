using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Abp.Application.Services;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace K9Abp.Wechat.Services
{
    public interface IWechatService: IApplicationService
    {
        List<WechatSettingOutput> GetWechatSettings();
        Task BindAsync(WechtLoginInput input);
    }
}