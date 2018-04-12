using System;
using Abp.Application.Services.Dto;
using K9Abp.Core.Chat;

namespace K9Abp.Application.Chat.Dto
{
    public class ChatMessageDto : EntityDto
    {
        public long UserId { get; set; }

        public int? TenantId { get; set; }

        public long TargetUserId { get; set; }

        public int? TargetTenantId { get; set; }

        public EChatSide Side { get; set; }

        public EChatMessageReadState ReadState { get; set; }

        public EChatMessageReadState ReceiverReadState { get; set; }

        public string Message { get; set; }
        
        public DateTime CreationTime { get; set; }

        public string SharedMessageId { get; set; }
    }
}
