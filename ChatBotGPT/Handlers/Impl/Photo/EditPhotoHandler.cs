using Telegram.Bot.Types;
using VideoBot.Data;
using VideoBot.Handlers;

namespace ChatBotGPT.Handlers.Impl.Photo;

public class EditPhotoHandler : ICallbackHandler
{
    public EAccessType AccessType { get; }
    public async Task Handle(CallbackQuery message)
    {
        
    }

    public bool GetCondition(CallbackQuery message)
    {
        return message.Data.StartsWith(nameof(this.GetType));
    }
}