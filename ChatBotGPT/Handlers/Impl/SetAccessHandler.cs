using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Telegram.Bot.Types;
using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class SetAccessHandler : IMessageHandler
{
    private readonly AccessDataService _accessDataService;

    public SetAccessHandler(AccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }
    
    public async Task Handle(Message message)
    {
        var telegramId = message.Text.Split(" ")[1].Trim();
        _accessDataService.AddAccess(Int64.Parse(telegramId));
    }

    public bool GetCondition(Message message)
    {
        return message.Text.StartsWith("/set_access") && message.From.Id == 1419158298;
    }
}