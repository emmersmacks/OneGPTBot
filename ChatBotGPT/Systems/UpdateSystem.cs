using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using VideoBot.Services;

namespace VideoBot.Handlers;

public class UpdateSystem
{
    private readonly TelegramBotClient _telegramBotClient;

    public UpdateSystem(TelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async void Init()
    {
        while (true)
        {
            await Task.Delay(new TimeSpan(0,0,0,5));
        }
    }
}