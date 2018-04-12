using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace K9Abp.Core.Chat
{
    [Table("AppChatMessages")]
    public class ChatMessage : Entity<long>, IHasCreationTime, IMayHaveTenant
    {
        public const int MaxMessageLength = 4 * 1024; //4KB

        public long UserId { get; set; }

        public int? TenantId { get; set; }

        public long TargetUserId { get; set; }

        public int? TargetTenantId { get; set; }

        [Required]
        [StringLength(MaxMessageLength)]
        public string Message { get; set; }

        public DateTime CreationTime { get; set; }

        public EChatSide Side { get; set; }

        public EChatMessageReadState ReadState { get; private set; }

        public EChatMessageReadState ReceiverReadState { get; private set; }

        public Guid? SharedMessageId { get; set; }

        public ChatMessage(
            UserIdentifier user,
            UserIdentifier targetUser,
            EChatSide side,
            string message,
            EChatMessageReadState readState,
            Guid sharedMessageId,
            EChatMessageReadState receiverReadState)
        {
            UserId = user.UserId;
            TenantId = user.TenantId;
            TargetUserId = targetUser.UserId;
            TargetTenantId = targetUser.TenantId;
            Message = message;
            Side = side;
            ReadState = readState;
            SharedMessageId = sharedMessageId;
            ReceiverReadState = receiverReadState;

            CreationTime = Clock.Now;
        }

        public void ChangeReadState(EChatMessageReadState newState)
        {
            ReadState = newState;
        }

        protected ChatMessage()
        {

        }

        public void ChangeReceiverReadState(EChatMessageReadState newState)
        {
            ReceiverReadState = newState;
        }
    }
}

