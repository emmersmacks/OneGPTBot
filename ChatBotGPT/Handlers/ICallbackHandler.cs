using Telegram.Bot.Types;

namespace VideoBot.Handlers;

public interface ICallbackHandler
{
    public Task Handle(CallbackQuery message);
    public bool GetCondition(CallbackQuery message);
}