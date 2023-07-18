using ChatBotGPT.Database;
using Telegram.Bot;
using Telegram.Bot.Types;
using VideoBot.Data;
using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class UnauthorizedAccessibleHandler : ITextAccessibleHandler
{
    private readonly UsersDataService _usersDataService;
    private readonly AccessDataService _accessDataService;
    private readonly TelegramMessagesService _messagesService;

    public UnauthorizedAccessibleHandler(
        UsersDataService usersDataService,
        AccessDataService accessDataService,
        TelegramMessagesService messagesService)
    {
        _usersDataService = usersDataService;
        _accessDataService = accessDataService;
        _messagesService = messagesService;
    }

    public EAccessType AccessType => EAccessType.UnauthorizedUser;

    public async Task Handle(Message message)
    {
        _messagesService.UnauthorizedMessage(message.Chat.Id);
    }

    public bool GetCondition(Message message)
    {
        return !_accessDataService.UserIsAvailable(message.From.Id);
    }
}