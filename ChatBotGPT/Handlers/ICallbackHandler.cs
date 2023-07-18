using Telegram.Bot.Types;
using VideoBot.Data;

namespace VideoBot.Handlers;

public interface ICallbackHandler
{
    public EAccessType AccessType { get; }
    public Task Handle(CallbackQuery message);
    public bool GetCondition(CallbackQuery message);
}