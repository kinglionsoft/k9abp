#if FEATURE_SIGNALR
using Abp.Application.Features;
using Abp.UI;
using MyCompanyName.AbpZeroTemplate.Features;

namespace MyCompanyName.AbpZeroTemplate.Chat
{
    public class ChatFeatureChecker : K9AbpDomainServiceBase, IChatFeatureChecker
    {
        private readonly IFeatureChecker _featureChecker;

        public ChatFeatureChecker(
            IFeatureChecker featureChecker
        )
        {
            _featureChecker = featureChecker;
        }

        public void CheckChatFeatures(int? sourceTenantId, int? targetTenantId)
        {
            CheckChatFeaturesInternal(sourceTenantId, targetTenantId, EChatSide.Sender);
            CheckChatFeaturesInternal(targetTenantId, sourceTenantId, EChatSide.Receiver);
        }

        private void CheckChatFeaturesInternal(int? sourceTenantId, int? targetTenantId, EChatSide side)
        {
            var localizationPosfix = side == EChatSide.Sender ? "ForSender" : "ForReceiver";
            if (sourceTenantId.HasValue)
            {
                if (!_featureChecker.IsEnabled(sourceTenantId.Value, AppFeatures.ChatFeature))
                {
                    throw new UserFriendlyException(L("ChatFeatureIsNotEnabled" + localizationPosfix));
                }

                if (targetTenantId.HasValue)
                {
                    if (sourceTenantId == targetTenantId)
                    {
                        return;
                    }

                    if (!_featureChecker.IsEnabled(sourceTenantId.Value, AppFeatures.TenantToTenantChatFeature))
                    {
                        throw new UserFriendlyException(L("TenantToTenantChatFeatureIsNotEnabled" + localizationPosfix));
                    }
                }
                else
                {
                    if (!_featureChecker.IsEnabled(sourceTenantId.Value, AppFeatures.TenantToHostChatFeature))
                    {
                        throw new UserFriendlyException(L("TenantToHostChatFeatureIsNotEnabled" + localizationPosfix));
                    }
                }
            }
            else
            {
                if (targetTenantId.HasValue)
                {
                    if (!_featureChecker.IsEnabled(targetTenantId.Value, AppFeatures.TenantToHostChatFeature))
                    {
                        throw new UserFriendlyException(L("TenantToHostChatFeatureIsNotEnabled" + (side == EChatSide.Sender ? "ForReceiver" : "ForSender")));
                    }
                }
            }
        }
    }
}
#endif
