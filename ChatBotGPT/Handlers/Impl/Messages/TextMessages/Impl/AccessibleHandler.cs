using ChatBotGPT;
using ChatBotGPT.ChatGPT;
using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VideoBot.Data;
using VideoBot.Services;
using Message = ChatBotGPT.ChatGPT.Message;

namespace VideoBot.Handlers.Messages.Impl;

public class AccessibleHandler : ITextAccessibleHandler
{
    private readonly GPTClient _gptClient;
    private readonly UsersDataService _usersDataService;
    private readonly AccessDataService _accessDataService;
    private readonly TelegramMessagesService _telegramMessagesService;

    public AccessibleHandler(
        GPTClient gptClient,
        UsersDataService usersDataService,
        AccessDataService accessDataService,
        TelegramMessagesService telegramMessagesService)
    {
        _gptClient = gptClient;
        _usersDataService = usersDataService;
        _accessDataService = accessDataService;
        _telegramMessagesService = telegramMessagesService;
    }

    public EAccessType AccessType => EAccessType.AuthorizedUser;

    public async Task Handle(Telegram.Bot.Types.Message message)
    {
        var user = _usersDataService.GetOrCreateUser(message);
        
        var response = await _gptClient.SendMessage(message.Text, user.Messages);

        if (response.Message.choices != null)
            _telegramMessagesService.SendGptResponse(response.Message, message, user);
        else
            _telegramMessagesService.SendGptError(response.Error, message, user);
    }

    public bool GetCondition(Telegram.Bot.Types.Message message)
    {
        return _accessDataService.UserIsAvailable(message.From.Id);
    }
}

