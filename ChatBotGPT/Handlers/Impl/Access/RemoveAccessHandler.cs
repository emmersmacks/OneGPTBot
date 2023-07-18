using ChatBotGPT.Database;
using Telegram.Bot.Types;
using VideoBot.Data;
using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class RemoveAccessHandler : ICommandHandler
{
    private readonly AccessDataService _accessDataService;
    private readonly ApplicationContext _applicationContext;

    public RemoveAccessHandler(
        AccessDataService accessDataService,
        ApplicationContext applicationContext)
    {
        _accessDataService = accessDataService;
        _applicationContext = applicationContext;
    }

    public EAccessType AccessType => EAccessType.Admin;

    public async Task Handle(Message message)
    {
        var username = message.Text.Split(" ")[1].Trim();
        var access = _applicationContext.Accesses.FirstOrDefault(a => a.Id == Int64.Parse(username));
        _applicationContext.Accesses.Remove(access);
        await _applicationContext.SaveChangesAsync();
    }

    public bool GetCondition(Message message)
    {
        return message.Text.Contains("/remove_access");
    }
}