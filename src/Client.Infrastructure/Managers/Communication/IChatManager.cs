using PaperStop.Application.Interfaces.Chat;
using PaperStop.Application.Models.Chat;
using PaperStop.Application.Responses.Identity;
using PaperStop.Shared.Wrapper;

namespace PaperStop.Client.Infrastructure.Managers.Communication;

public interface IChatManager : IManager
{
    Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

    Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

    Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
}