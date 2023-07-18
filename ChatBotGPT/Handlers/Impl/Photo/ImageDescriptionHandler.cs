using Telegram.Bot.Types;
using VideoBot.Data;

namespace VideoBot.Handlers.Messages.Impl;

public class ImageDescriptionHandler : IPendingHandler
{
    public async Task Handle(Message message)
    {
        
    }

    public bool GetCondition(Message message)
    {
        return true;
    }
}