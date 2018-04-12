using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using K9Abp.Core.Authorization.Users;
using K9Abp.Core.MultiTenancy;

namespace K9Abp.Core.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info);
    }
}

