using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Telegram.Bot.Types;
using VideoBot.Data;
using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class SetAccessHandler : ICommandHandler
{
    private readonly AccessDataService _accessDataService;
    
    public EAccessType AccessType => EAccessType.Admin;

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
        return message.Text.StartsWith("/set_access");
    }
}