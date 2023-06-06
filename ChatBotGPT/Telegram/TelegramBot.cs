using ChatBotGPT.ChatGPT;
using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using VideoBot.Handlers;

namespace ChatBotGPT;

public class TelegramBot
{
    private readonly MessagesReceiver _messagesReceiver;
    private readonly TelegramBotClient _telegramBotClient;
    private readonly UpdateSystem _updateSystem;
    private readonly GPTClient _gptClient;

    public TelegramBot(MessagesReceiver messagesReceiver, TelegramBotClient telegramBotClient, UpdateSystem updateSystem, GPTClient gptClient)
    {
        _messagesReceiver = messagesReceiver;
        _telegramBotClient = telegramBotClient;
        _updateSystem = updateSystem;
        _gptClient = gptClient;
    }

    public async Task Start()
    {
        _telegramBotClient.OnMessage += _messagesReceiver.OnMessageReceived;
        _telegramBotClient.OnCallbackQuery += _messagesReceiver.OnCallbackReceived;
        
        _telegramBotClient.StartReceiving();
        _updateSystem.Init();
        
        Console.WriteLine("Bot started");
        await Task.Delay(-1);
        _telegramBotClient.StopReceiving();
    }
}