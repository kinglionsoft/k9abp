using Abp.Domain.Repositories;
using K9Abp.Application;
namespace K9AbpPlugin.Broadband.User
{
    public class UserAppService: K9AbpAppServiceBase
    {
        private readonly IRepository<BroadbandUser> _repository;

        public UserAppService(IRepository<BroadbandUser> repository)
        {
            _repository = repository;
        }

        #region 导入

        

        #endregion
    }
}