using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace VideoBot.Handlers;

public interface IMessageHandler
{
    public Task Handle(Message message);
    public bool GetCondition(Message message);
}