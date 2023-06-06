using ChatBotGPT.Database;
using Telegram.Bot;
using Telegram.Bot.Types;
using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class UnauthorizedMessageHandler : IMessageHandler
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly UsersDataService _usersDataService;
    private readonly AccessDataService _accessDataService;

    public UnauthorizedMessageHandler(
        TelegramBotClient telegramBotClient, 
        UsersDataService usersDataService,
        AccessDataService accessDataService)
    {
        _telegramBotClient = telegramBotClient;
        _usersDataService = usersDataService;
        _accessDataService = accessDataService;
    }

    public async Task Handle(Message message)
    {
        await _telegramBotClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "У тебя нет доступа к чату. \nЕсли ты уверен, что он у тебя должен быть, то напиши мне: @ekb_yan_vishnevskiy1"
        );
    }

    public bool GetCondition(Message message)
    {
        return !_accessDataService.UserIsAvailable(message.From.Id) && message.From.Id != 1419158298;
    }
}