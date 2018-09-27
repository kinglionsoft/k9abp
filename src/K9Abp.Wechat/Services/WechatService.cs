using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using K9Abp.Application;
using K9Abp.Core.Configuration;

namespace K9Abp.Wechat.Services
{
    public class WechatService : K9AbpAppServiceBase, IWechatService
    {
        private readonly IRepository<Setting, long> _repository;
        private readonly IRepository<UserLogin, long> _loginRepository;

        public WechatService(IRepository<Setting, long> repository, IRepository<UserLogin, long> loginRepository)
        {
            _repository = repository;
            _loginRepository = loginRepository;
        }

        /// <summary>
        /// 获取所有微信公众号配置信息
        /// </summary>
        /// <returns></returns>
        public List<WechatSettingOutput> GetWechatSettings()
        {
            string[] names =
            {
                AppSettings.TenantManagement.WechatAppId,
                AppSettings.TenantManagement.WechatAppSecret,
                AppSettings.TenantManagement.WechatAppName
            };
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return _repository.GetAll()
                    .Where(x => names.Contains(x.Name) && x.TenantId != null)
                    .GroupBy(x => x.TenantId)
                    .Select(x => new WechatSettingOutput
                    {
                        AppId = x.First(n => n.Name == AppSettings.TenantManagement.WechatAppId).Value,
                        AppSecret = x.First(n => n.Name == AppSettings.TenantManagement.WechatAppSecret).Value,
                        AppName = x.First(n => n.Name == AppSettings.TenantManagement.WechatAppName).Value
                    }).ToList();
            }
        }

        public async Task BindAsync(WechtLoginInput input)
        {
            var login = input.MapTo<UserLogin>();
            await _loginRepository.InsertAsync(login);
        }
    }
}