using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using K9Abp.Application.Chat.Dto;

namespace K9Abp.Application.Chat
{
    public interface IChatAppService : IApplicationService
    {
        GetUserChatFriendsWithSettingsOutput GetUserChatFriendsWithSettings();

        Task<ListResultDto<ChatMessageDto>> GetUserChatMessages(GetUserChatMessagesInput input);

        Task MarkAllUnreadMessagesOfUserAsRead(MarkAllUnreadMessagesOfUserAsReadInput input);
    }
}

