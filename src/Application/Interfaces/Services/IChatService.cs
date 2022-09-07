using PaperStop.Application.Interfaces.Chat;
using PaperStop.Application.Models.Chat;
using PaperStop.Application.Responses.Identity;
using PaperStop.Shared.Wrapper;

namespace PaperStop.Application.Interfaces.Services;

public interface IChatService
{
    Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

    Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

    Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
}
