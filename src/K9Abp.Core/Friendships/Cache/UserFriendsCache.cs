﻿using System.Linq;
using Abp;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;
using Abp.Threading;
using K9Abp.Core.Authorization.Users;
using K9Abp.Core.Chat;

namespace K9Abp.Core.Friendships.Cache
{
    public class UserFriendsCache : IUserFriendsCache, ISingletonDependency
    {
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<Friendship, long> _friendshipRepository;
        private readonly IRepository<ChatMessage, long> _chatMessageRepository;
        private readonly ITenantCache _tenantCache;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly object _syncObj = new object();

        public UserFriendsCache(
            ICacheManager cacheManager,
            IRepository<Friendship, long> friendshipRepository,
            IRepository<ChatMessage, long> chatMessageRepository,
            ITenantCache tenantCache,
            UserManager userManager,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _cacheManager = cacheManager;
            _friendshipRepository = friendshipRepository;
            _chatMessageRepository = chatMessageRepository;
            _tenantCache = tenantCache;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [UnitOfWork]
        public virtual UserWithFriendsCacheItem GetCacheItem(UserIdentifier userIdentifier)
        {
            return _cacheManager
                .GetCache(FriendCacheItem.CacheName)
                .Get<string, UserWithFriendsCacheItem>(userIdentifier.ToUserIdentifierString(), f => GetUserFriendsCacheItemInternal(userIdentifier));
        }

        public virtual UserWithFriendsCacheItem GetCacheItemOrNull(UserIdentifier userIdentifier)
        {
            return _cacheManager
                .GetCache(FriendCacheItem.CacheName)
                .GetOrDefault<string, UserWithFriendsCacheItem>(userIdentifier.ToUserIdentifierString());
        }

        [UnitOfWork]
        public virtual void ResetUnreadMessageCount(UserIdentifier userIdentifier, UserIdentifier friendIdentifier)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null)
            {
                return;
            }

            lock (_syncObj)
            {
                var friend = user.Friends.FirstOrDefault(
                     f => f.FriendUserId == friendIdentifier.UserId &&
                     f.FriendTenantId == friendIdentifier.TenantId
                 );

                if (friend == null)
                {
                    return;
                }

                friend.UnreadMessageCount = 0;
            }
        }

        [UnitOfWork]
        public virtual void IncreaseUnreadMessageCount(UserIdentifier userIdentifier, UserIdentifier friendIdentifier, int change)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null)
            {
                return;
            }

            lock (_syncObj)
            {
                var friend = user.Friends.FirstOrDefault(
                     f => f.FriendUserId == friendIdentifier.UserId &&
                     f.FriendTenantId == friendIdentifier.TenantId
                );

                if (friend == null)
                {
                    return;
                }

                friend.UnreadMessageCount += change;
            }
        }

        public void AddFriend(UserIdentifier userIdentifier, FriendCacheItem friend)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null)
            {
                return;
            }

            lock (_syncObj)
            {
                if (!user.Friends.ContainsFriend(friend))
                {
                    user.Friends.Add(friend);
                }
            }
        }

        public void RemoveFriend(UserIdentifier userIdentifier, FriendCacheItem friend)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null)
            {
                return;
            }

            lock (_syncObj)
            {
                if (user.Friends.ContainsFriend(friend))
                {
                    user.Friends.Remove(friend);
                }
            }
        }

        public void UpdateFriend(UserIdentifier userIdentifier, FriendCacheItem friend)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null)
            {
                return;
            }

            lock (_syncObj)
            {
                var existingFriendIndex = user.Friends.FindIndex(
                    f => f.FriendUserId == friend.FriendUserId &&
                    f.FriendTenantId == friend.FriendTenantId
                );

                if (existingFriendIndex >= 0)
                {
                    user.Friends[existingFriendIndex] = friend;
                }
            }
        }

        [UnitOfWork]
        protected virtual UserWithFriendsCacheItem GetUserFriendsCacheItemInternal(UserIdentifier userIdentifier)
        {
            var tenancyName = userIdentifier.TenantId.HasValue
                ? _tenantCache.GetOrNull(userIdentifier.TenantId.Value)?.TenancyName
                : null;

            using (_unitOfWorkManager.Current.SetTenantId(userIdentifier.TenantId))
            {
                var friendCacheItems =
                    (from friendship in _friendshipRepository.GetAll()
                     join chatMessage in _chatMessageRepository.GetAll() on
                     new { UserId = userIdentifier.UserId, TenantId = userIdentifier.TenantId, TargetUserId = friendship.FriendUserId, TargetTenantId = friendship.FriendTenantId, EChatSide = EChatSide.Receiver } equals
                     new { UserId = chatMessage.UserId, TenantId = chatMessage.TenantId, TargetUserId = chatMessage.TargetUserId, TargetTenantId = chatMessage.TargetTenantId, EChatSide = chatMessage.Side } into chatMessageJoined
                     where friendship.UserId == userIdentifier.UserId
                     select new FriendCacheItem
                     {
                         FriendUserId = friendship.FriendUserId,
                         FriendTenantId = friendship.FriendTenantId,
                         State = friendship.State,
                         FriendUserName = friendship.FriendUserName,
                         FriendTenancyName = friendship.FriendTenancyName,
                         FriendProfilePictureId = friendship.FriendProfilePictureId,
                         UnreadMessageCount = chatMessageJoined.Count(cm => cm.ReadState == EChatMessageReadState.Unread)
                     }).ToList();

                var user = AsyncHelper.RunSync(() => _userManager.FindByIdAsync(userIdentifier.UserId.ToString()));

                return new UserWithFriendsCacheItem
                {
                    TenantId = userIdentifier.TenantId,
                    UserId = userIdentifier.UserId,
                    TenancyName = tenancyName,
                    UserName = user.UserName,
                    ProfilePictureId = user.ProfilePictureId,
                    Friends = friendCacheItems
                };
            }
        }
    }
}
