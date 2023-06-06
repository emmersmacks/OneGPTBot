using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class ClearHistoryHandler : IMessageHandler
{
    private readonly UsersDataService _usersDataService;
    private readonly TelegramBotClient _telegramBotClient;
    private readonly AccessDataService _accessDataService;

    public ClearHistoryHandler(UsersDataService usersDataService, TelegramBotClient telegramBotClient, AccessDataService accessDataService)
    {
        _usersDataService = usersDataService;
        _telegramBotClient = telegramBotClient;
        _accessDataService = accessDataService;
    }
    
    public async Task Handle(Message message)
    {
        var user = _usersDataService.GetUser(message.From.Id);
        user.Messages = new List<string>();
        _usersDataService.UpdateUser(user);
        
        await _telegramBotClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "История очищена!"
        );
    }

    public bool GetCondition(Message message)
    {
        return message.Text != null && _accessDataService.UserIsAvailable(message.From.Id) && message.Text.StartsWith("/clear_history");
    }
}