using Telegram.Bot.Types;
using VideoBot.Data;
using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class TextHandler : ITextAccessibleHandler
{
    private readonly TelegramMessagesService _telegramMessagesService;

    public TextHandler(TelegramMessagesService telegramMessagesService)
    {
        _telegramMessagesService = telegramMessagesService;
    }

    public async Task Handle(Message message)
    {
        _telegramMessagesService.SendStartMessage(message);
    }

    public bool GetCondition(Message message)
    {
        return message.Text == "Hello";
    }

    public EAccessType AccessType => EAccessType.AuthorizedUser;
}