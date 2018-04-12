namespace K9Abp.Web.Core.Chat.SignalR
{
    public class SendFriendshipRequestInput
    {
        public long UserId { get; set; }

        public int? TenantId { get; set; }
    }
}
