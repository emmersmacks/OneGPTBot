using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace VideoBot.Services;

public class TelegramMessagesService
{
    private readonly TelegramBotClient _telegramBotClient;

    public TelegramMessagesService(TelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async Task SendStartMessage(Message message)
    {
        await _telegramBotClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Привет! Это бот для общения с ChatGPT. \nЗадай любой вопрос и я постараюсь на него ответить!"
        );
    }
}