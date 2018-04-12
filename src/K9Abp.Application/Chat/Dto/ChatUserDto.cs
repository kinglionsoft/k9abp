using System;
using Abp.Application.Services.Dto;
using K9Abp.Core.Friendships;

namespace K9Abp.Application.Chat.Dto
{
    public class ChatUserDto : EntityDto<long>
    {
        public int? TenantId { get; set; }

        public Guid? ProfilePictureId { get; set; }

        public string UserName { get; set; }

        public string TenancyName { get; set; }

        public int UnreadMessageCount { get; set; }

        public bool IsOnline { get; set; }

        public EFriendshipState State { get; set; }
    }
}
