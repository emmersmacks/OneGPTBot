using ChatBotGPT.ChatGPT;
using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using VideoBot.Data;
using VideoBot.Handlers;
using VideoBot.Services;

namespace ChatBotGPT;

public class TelegramBot
{
    private readonly MessagesReceiver _messagesReceiver;
    private readonly TelegramBotClient _telegramBotClient;


    public TelegramBot(
        MessagesReceiver messagesReceiver,
        ConfigService configService,
        TelegramBotClient telegramBotClient)
    {
        _messagesReceiver = messagesReceiver;
        _telegramBotClient = telegramBotClient;
    }

    public async Task Start()
    {
        _telegramBotClient.OnMessage += _messagesReceiver.OnMessageReceived;
        _telegramBotClient.OnCallbackQuery += _messagesReceiver.OnCallbackReceived;
        
        _telegramBotClient.StartReceiving();
        
        Console.WriteLine("Bot started");
        await Task.Delay(-1);
        _telegramBotClient.StopReceiving();
    }
}