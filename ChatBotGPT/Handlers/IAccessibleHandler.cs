using Telegram.Bot.Args;
using Telegram.Bot.Types;
using VideoBot.Data;

namespace VideoBot.Handlers;

public interface IAccessibleHandler : ICheckedHandler
{
    public EAccessType AccessType { get; }
}