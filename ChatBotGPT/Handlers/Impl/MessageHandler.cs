using ChatBotGPT.ChatGPT;
using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class MessageHandler : IMessageHandler
{
    private readonly GPTClient _gptClient;
    private readonly TelegramBotClient _telegramBotClient;
    private readonly UsersDataService _usersDataService;
    private readonly AccessDataService _accessDataService;

    public MessageHandler(GPTClient gptClient, TelegramBotClient telegramBotClient, UsersDataService usersDataService, AccessDataService accessDataService)
    {
        _gptClient = gptClient;
        _telegramBotClient = telegramBotClient;
        _usersDataService = usersDataService;
        _accessDataService = accessDataService;
    }

    public async Task Handle(Message message)
    {
        var user = _usersDataService.GetOrCreateUser(message);
        
        var response = await _gptClient.SendMessage(message.Text, user.Messages);

        user.Messages.Add($"user:{message.Text}");
        user.Messages.Add($"{response.choices[0].message.role}:{response.choices[0].message.content}");

        var responseText = response.choices[0].message.content;
        //responseText = responseText.Replace("#", @"\#");
        await _telegramBotClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: responseText,
            ParseMode.Markdown
        );
    }

    public bool GetCondition(Message message)
    {
        return _accessDataService.UserIsAvailable(message.From.Id) || message.From.Id == 1419158298;
    }
}