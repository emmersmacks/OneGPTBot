using ChatBotGPT;
using ChatBotGPT.ChatGPT;
using ChatBotGPT.Database.Models;
using ChatBotGPT.Handlers.Impl.Photo;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using VideoBot.Handlers.Callbacks;
using Message = ChatBotGPT.ChatGPT.Message;
using MessageType = ChatBotGPT.ChatGPT.MessageType;

namespace VideoBot.Services;

public class TelegramMessagesService
{
    private readonly TelegramBotClient _telegramBot;
    private readonly TelegramKeyboard _telegramKeyboard;

    public TelegramMessagesService(TelegramBotClient telegramBot,
        TelegramKeyboard telegramKeyboard)
    {
        _telegramBot = telegramBot;
        _telegramKeyboard = telegramKeyboard;
    }

    public async Task SendStartMessage(Telegram.Bot.Types.Message message)
    {
        await _telegramBot.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Привет! Это бот для общения с ChatGPT. \nЗадай любой вопрос и я постараюсь на него ответить!"
        );
    }

    public async Task SendGptResponse(Message response, Telegram.Bot.Types.Message message, UserModel user)
    {
        user.Messages.Add($"user:{message.Text}");
        user.Messages.Add($"{response.choices[0].message.role}:{response.choices[0].message.content}");

        var responseText = response.choices[0].message.content;

        await _telegramBot.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: responseText,
            ParseMode.Markdown
        );
    }
    
    public async Task SendGptError(Error response, Telegram.Bot.Types.Message message, UserModel user)
    {
        var responseText = response.error.message;
        
        user.Messages.Add($"user:{message.Text}");
        user.Messages.Add($"assistant:{responseText}");

        await _telegramBot.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: responseText,
            ParseMode.Markdown
        );
    }

    public async void UnauthorizedMessage(long chatId)
    {
        await _telegramBot.SendTextMessageAsync(
            chatId: chatId,
            text: "У тебя нет доступа к чату. \nЕсли ты уверен, что он у тебя должен быть, то напиши мне: @ekb_yan_vishnevskiy1"
        );
    }

    public async void SendPhotoAnswerMessage(CallbackQuery message)
    {
        await _telegramBot.AnswerCallbackQueryAsync(message.Id);
        await _telegramBot.EditMessageTextAsync(message.Message.Chat.Id,
            message.Message.MessageId,
            "Чтобы я мог сгенерировать для вас качественное изображение, мне нужно получить от вас дополнительную информацию. Напишите пожалуйста, что вы хотите видеть на изображении?");
    }

    public async void SendPhotoKeyboard(Telegram.Bot.Types.Message message)
    {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                _telegramKeyboard.GetButton<GeneratePhotoHandler>("Сгенерировать изображение"),
            },
            new[]
            {
                _telegramKeyboard.GetButton<EditPhotoHandler>("Отредактировать своё изображение") 
            }
        });
        await _telegramBot.SendTextMessageAsync(
            message.Chat,
            "Выберите опцию",
            replyMarkup: inlineKeyboard);
    }

    public void SendUnknownError(Telegram.Bot.Types.Message message)
    {
        _telegramBot.SendTextMessageAsync(message.Chat.Id, "Неизвестная ошибка");
    }

    public void SendPhoto(Telegram.Bot.Types.Message message, InputOnlineFile photo)
    {
        _telegramBot.SendPhotoAsync(message.Chat.Id, photo);
    }

    public async void SendHistoryClearMessage(Telegram.Bot.Types.Message message)
    {
        await _telegramBot.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "История очищена!"
        );
    }
}