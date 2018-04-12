using Abp.Notifications;
using K9Abp.Application.Dto;

namespace K9Abp.Application.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}
