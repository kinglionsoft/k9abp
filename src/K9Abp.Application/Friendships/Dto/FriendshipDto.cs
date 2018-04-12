using System;
using K9Abp.Core.Friendships;

namespace K9Abp.Application.Friendships.Dto
{
    public class FriendDto
    {
        public long FriendUserId { get; set; }

        public int? FriendTenantId { get; set; }

        public string FriendUserName { get; set; }

        public string FriendTenancyName { get; set; }

        public Guid? FriendProfilePictureId { get; set; }

        public int UnreadMessageCount { get; set; }

        public bool IsOnline { get; set; }

        public EFriendshipState State { get; set; }
    }
}

