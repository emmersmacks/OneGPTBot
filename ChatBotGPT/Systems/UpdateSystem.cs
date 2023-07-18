using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using VideoBot.Services;

namespace VideoBot.Handlers;

public class UpdateSystem
{
    public UpdateSystem()
    {
    }

    public async void Init()
    {
        while (true)
        {
            await Task.Delay(new TimeSpan(0,0,0,1));
        }
    }
}