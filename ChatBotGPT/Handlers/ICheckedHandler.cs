using Telegram.Bot.Types;

namespace VideoBot.Handlers;

public interface ICheckedHandler : IHandler
{
    public Task Handle(Message message);
    public bool GetCondition(Message message);
}