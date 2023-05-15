using ChatBotGPT.ChatGPT;
using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

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
        var message = messageEventArgs.Message;

        if (message.Text == "/start")
        {
            await _client.SendTextMessageAsync(
                chatId: messageEventArgs.Message.Chat.Id,
                text: "Привет! Это бот для общения с ChatGPT. \nЗадай любой вопрос и я постараюсь на него ответить!"
            );
        }
        else if(message.Text.Contains("/set_access") && message.From.Id == 1419158298)
        {
            var username = message.Text.Split(" ")[1].Trim();
            using (var context = new ApplicationContext())
            {
                context.Accesses.Add(new AccessModel(){Username = username});
                await context.SaveChangesAsync();
            }
        }
        else if(message.Text.Contains("/remove_access") && message.From.Id == 1419158298)
        {
            var username = message.Text.Split(" ")[1].Trim();
            using (var context = new ApplicationContext())
            {
                var access = context.Accesses.FirstOrDefault(a => a.Username == username);
                context.Accesses.Remove(access);
                await context.SaveChangesAsync();
            }
        }
        else if (UserIsAvailable(message.From) || message.From.Id == 1419158298)
        {
            using (var context = new ApplicationContext())
            {
                var user = context.Users.FirstOrDefault(a => a.Id == message.From.Id);
                if (user == null)
                {
                    var newUser = new UserModel()
                    {
                        Id = message.From.Id,
                        FirstName = message.From.FirstName,
                        LastName = message.From.LastName,
                        Username = message.From.Username,
                        Messages = new List<string>{"assistant:Привет! Это бот для общения с ChatGPT. \nЗадай любой вопрос и я постараюсь на него ответить!"}
                    };
                    context.Users.Add(newUser);
                    user = newUser;
                }
                
                var responce = await _gpt.SendMessage(message.Text, user.Messages);
                await _client.SendTextMessageAsync(
                    chatId: messageEventArgs.Message.Chat.Id,
                    text: responce.choices[0].message.content
                );
                user.Messages.Add($"{responce.choices[0].message.role}:{responce.choices[0].message.content}");
                await context.SaveChangesAsync();
            }
        }
        else
        {
            await _client.SendTextMessageAsync(
                chatId: messageEventArgs.Message.Chat.Id,
                text: "У тебя нет доступа к чату. \nЕсли ты уверен, что он у тебя должен быть, то напиши мне: @ekb_yan_vishnevskiy1"
            );
        }
    }

    private bool UserIsAvailable(User user)
    {
        using (var context = new ApplicationContext())
        {
            Console.WriteLine(context.Accesses);
            var access = context.Accesses.FirstOrDefault(a => a.Username == user.Username);
            return access != null;
        }
    }
}