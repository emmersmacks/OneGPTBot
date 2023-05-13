using ChatBotGPT.ChatGPT;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ChatBotGPT;

public class TelegramBot
{
    private readonly TelegramBotClient _client;
    private GPTClient _gpt;
    private const string TOKEN = "6118348079:AAGaRzbihjeanrXklNfecWQtPXi4ud6pVZ4";

    public TelegramBot()
    {
        _client = new TelegramBotClient(TOKEN);
        _gpt = new GPTClient();
    }
    
    public async void Start()
    {
        _client.OnMessage += OnMessage;
        _client.OnMessageEdited += OnMessage;
        
        //_client.OnCallbackQuery += callbacksHandler.OnCallbackReceived;
        
        _client.StartReceiving();

        Console.WriteLine("Bot started");
        Console.WriteLine($"Server datetime: {DateTime.Now}");
        Console.ReadLine();
        _client.StopReceiving();
    }

    private async void OnMessage(object? sender, MessageEventArgs messageEventArgs)
    {
        
        var message = messageEventArgs.Message.Text;
        
        if (message != "" && message != "/start")
        {
            var responce = await _gpt.SendMessage(message, messageEventArgs.Message.Chat.Id.ToString());
            await _client.SendTextMessageAsync(
                chatId: messageEventArgs.Message.Chat.Id,
                text: responce.choices[0].message.content
            );
        }
    }
}