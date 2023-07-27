using ChatBotGPT.Handlers.Impl.Photo;
using Telegram.Bot;
using Telegram.Bot.Types;
using VideoBot.Data;
using VideoBot.Handlers.Messages.Impl;
using VideoBot.Services;

namespace VideoBot.Handlers.Callbacks;

public class GeneratePhotoHandler : ICallbackHandler
{
    private readonly TelegramMessagesService _telegramMessagesService;
    private readonly UsersDataService _usersDataService;
    public EAccessType AccessType => EAccessType.AuthorizedUser;

    public GeneratePhotoHandler(
        TelegramMessagesService telegramMessagesService,
        UsersDataService usersDataService)
    {
        _telegramMessagesService = telegramMessagesService;
        _usersDataService = usersDataService;
    }

    public async Task Handle(CallbackQuery message)
    {
        _usersDataService.SetUserHandler<PhotoDescriptionHandler>(message.From.Id);
        _telegramMessagesService.SendPhotoAnswerMessage(message);
    }

    public bool GetCondition(CallbackQuery message)
    {
        return message.Data.StartsWith(this.GetType().Name);
    }
}