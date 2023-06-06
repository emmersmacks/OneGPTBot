using ChatBotGPT.ChatGPT;
using Telegram.Bot;
using Telegram.Bot.Types;
using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class PictureHandler : IMessageHandler
{
    private readonly UsersDataService _usersDataService;
    private readonly GPTClient _gptClient;
    private readonly TelegramBotClient _telegramBotClient;
    private readonly AccessDataService _accessDataService;

    public PictureHandler(UsersDataService usersDataService, GPTClient gptClient, TelegramBotClient telegramBotClient, AccessDataService accessDataService)
    {
        _usersDataService = usersDataService;
        _gptClient = gptClient;
        _telegramBotClient = telegramBotClient;
        _accessDataService = accessDataService;
    }
    
    public async Task Handle(Message message)
    {
        var request = message.Text.Replace("/pic ", "");
        var photo = await _gptClient.GeneratePhoto(request, 1);
        if(photo != null)
            _telegramBotClient.SendPhotoAsync(message.Chat.Id, photo);
        else
        {
            _telegramBotClient.SendTextMessageAsync(message.Chat.Id, "Неизвестная ошибка");
        }
    }

    public bool GetCondition(Message message)
    {
        var dfsd = message.Text.StartsWith("/pic");
        return message.Text != null && _accessDataService.UserIsAvailable(message.From.Id) && message.Text.StartsWith("/pic");
    }
}