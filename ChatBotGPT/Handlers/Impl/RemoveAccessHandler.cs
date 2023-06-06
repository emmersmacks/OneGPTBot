using ChatBotGPT.Database;
using Telegram.Bot.Types;

namespace VideoBot.Handlers.Messages.Impl;

public class RemoveAccessHandler : IMessageHandler
{
    public async Task Handle(Message message)
    {
        var username = message.Text.Split(" ")[1].Trim();
        using (var context = new ApplicationContext())
        {
            var access = context.Accesses.FirstOrDefault(a => a.Id == Int64.Parse(username));
            context.Accesses.Remove(access);
            await context.SaveChangesAsync();
        }
    }

    public bool GetCondition(Message message)
    {
        return message.Text.Contains("/remove_access") && message.From.Id == 1419158298;
    }
}