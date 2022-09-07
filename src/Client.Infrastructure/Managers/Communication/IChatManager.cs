using FPAAgentura.Application.Interfaces.Chat;
using FPAAgentura.Application.Models.Chat;
using FPAAgentura.Application.Responses.Identity;
using FPAAgentura.Shared.Wrapper;

namespace Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}