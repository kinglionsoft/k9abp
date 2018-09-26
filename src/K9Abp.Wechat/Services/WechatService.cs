using System.Collections.Generic;
using System.Linq;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using K9Abp.Application;
using K9Abp.Core.Configuration;

namespace K9Abp.Wechat.Services
{
    internal class WechatService : K9AbpAppServiceBase, IWechatService
    {
        private readonly IRepository<Setting, long> _repository;

        public WechatService(IRepository<Setting, long> repository)
        {
            _repository = repository;
        }

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
    }
}