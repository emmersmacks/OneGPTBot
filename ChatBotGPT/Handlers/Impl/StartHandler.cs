using Telegram.Bot;
using Telegram.Bot.Types;

using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class StartHandler : IMessageHandler
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly TelegramMessagesService _telegramMessagesService;

    public StartHandler(TelegramBotClient? telegramBotClient, TelegramMessagesService telegramMessagesService)
    {
        _telegramBotClient = telegramBotClient;
        _telegramMessagesService = telegramMessagesService;
    }

    public async Task Handle(Message message)
    {
        await _telegramMessagesService.SendStartMessage(message);
    }

    public bool GetCondition(Message message)
    {
        return message.Text != null && message.Text == "/start";
    }
}