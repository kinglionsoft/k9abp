﻿using System.Threading.Tasks;
using Abp.Dependency;

namespace K9Abp.Web.Core.Authentication.External
{
    public abstract class ExternalAuthProviderApiBase : IExternalAuthProviderApi, ITransientDependency
    {
        public ExternalLoginProviderInfo ProviderInfo { get; set; }

        public void Initialize(ExternalLoginProviderInfo providerInfo)
        {
            ProviderInfo = providerInfo;
        }

        public virtual async Task<bool> IsValidUser(string userId, string accessCode)
        {
            var userInfo = await GetUserInfo(accessCode);
            return userInfo.ProviderKey == userId;
        }

        public abstract Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);
    }
}

