using ChatBotGPT.ChatGPT;
using Telegram.Bot;
using VideoBot.Data;
using VideoBot.Handlers;
using VideoBot.Services;
using Message = Telegram.Bot.Types.Message;

namespace ChatBotGPT.Handlers.Impl.Photo;

public class PhotoDescriptionHandler : ICheckedHandler
{
    private readonly GPTClient _gptClient;
    private readonly TelegramMessagesService _telegramMessagesService;

    public PhotoDescriptionHandler(
        GPTClient gptClient,
        TelegramMessagesService telegramMessagesService)
    {
        _gptClient = gptClient;
        _telegramMessagesService = telegramMessagesService;
    }
    
    public async Task Handle(Message message)
    {
        var photo = await _gptClient.GeneratePhoto(message.Text, 1);
        if (photo != null)
            _telegramMessagesService.SendPhoto(message, photo);
        else
            _telegramMessagesService.SendUnknownError(message);

    }

    public bool GetCondition(Message message)
    {
        return true;
    }
}