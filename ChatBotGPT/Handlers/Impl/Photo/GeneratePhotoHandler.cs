using Telegram.Bot;
using Telegram.Bot.Types;
using VideoBot.Data;
using VideoBot.Services;

namespace VideoBot.Handlers.Callbacks;

public class GeneratePhotoHandler : ICallbackHandler
{
    private readonly TelegramMessagesService _telegramMessagesService;
    public EAccessType AccessType => EAccessType.AuthorizedUser;

    public GeneratePhotoHandler(
        TelegramMessagesService telegramMessagesService)
    {
        _telegramMessagesService = telegramMessagesService;
    }

    public async Task Handle(CallbackQuery message)
    {
        _telegramMessagesService.SendPhotoAnswerMessage(message);
    }

    public bool GetCondition(CallbackQuery message)
    {
        return message.Data.StartsWith(this.GetType().Name);
    }
}