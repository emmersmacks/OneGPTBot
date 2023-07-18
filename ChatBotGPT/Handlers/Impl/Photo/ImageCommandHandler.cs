using ChatBotGPT.ChatGPT;
using ChatBotGPT.Handlers.Impl.Photo;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using VideoBot.Data;
using VideoBot.Handlers.Callbacks;
using VideoBot.Services;
using Message = Telegram.Bot.Types.Message;

namespace VideoBot.Handlers.Messages.Impl;

public class ImageCommandHandler : ICommandHandler
{
    private readonly UsersDataService _usersDataService;
    private readonly GPTClient _gptClient;
    private readonly AccessDataService _accessDataService;
    private readonly TelegramMessagesService _telegramMessagesService;

    public ImageCommandHandler(
        UsersDataService usersDataService,
        GPTClient gptClient,
        AccessDataService accessDataService,
        TelegramMessagesService telegramMessagesService)
    {
        _usersDataService = usersDataService;
        _gptClient = gptClient;
        _accessDataService = accessDataService;
        _telegramMessagesService = telegramMessagesService;
    }

    public EAccessType AccessType => EAccessType.AuthorizedUser;
    
    public async Task Handle(Message message)
    {
        _telegramMessagesService.SendPhotoKeyboard(message);
    }

    public bool GetCondition(Message message)
    {
        return message.Text.StartsWith("/pic");
    }
}