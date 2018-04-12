﻿using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Notifications;

namespace K9Abp.Application.Notifications.Dto
{
    public class GetNotificationsOutput : PagedResultDto<UserNotification>
    {
        public int UnreadCount { get; set; }

        public GetNotificationsOutput(int totalCount, int unreadCount, List<UserNotification> notifications)
            :base(totalCount, notifications)
        {
            UnreadCount = unreadCount;
        }
    }
}
