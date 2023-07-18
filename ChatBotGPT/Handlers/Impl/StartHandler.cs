using Telegram.Bot;
using Telegram.Bot.Types;
using VideoBot.Data;
using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class StartHandler : ICommandHandler
{
    private readonly TelegramMessagesService _telegramMessagesService;

    public EAccessType AccessType => EAccessType.UnauthorizedUser;
    
    public StartHandler(TelegramMessagesService telegramMessagesService)
    {
        _telegramMessagesService = telegramMessagesService;
    }

    public async Task Handle(Message message)
    {
        await _telegramMessagesService.SendStartMessage(message);
    }

    public bool GetCondition(Message message)
    {
        return message.Text.StartsWith("/start");
    }
}