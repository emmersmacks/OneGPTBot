using ChatBotGPT.ChatGPT;
using ChatBotGPT.Database;
using ChatBotGPT.Handlers.Impl.Photo;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using VideoBot.Data;
using VideoBot.Handlers;
using VideoBot.Handlers.Callbacks;
using VideoBot.Handlers.Messages.Impl;
using VideoBot.Services;

namespace ChatBotGPT;

public class Setup
{
    public ServiceProvider Init()
    {
        var services = new ServiceCollection();
        
        var configService = new ConfigService();
        var token = configService.GetString(ConfigNames.TelegramToken);
        services.AddSingleton(new TelegramBotClient(token));
        services.AddSingleton(configService);

        services.AddScoped<ApplicationContext>();
        services.AddSingleton<TelegramMessagesService>();
        services.AddSingleton<UsersDataService>();
        services.AddSingleton<AccessDataService>();
        services.AddSingleton<TelegramKeyboard>();

        services.AddSingleton<ITextAccessibleHandler, UnauthorizedAccessibleHandler>();
        services.AddSingleton<ITextAccessibleHandler, AccessibleHandler>();
        
        services.AddSingleton<IPendingHandler, PhotoDescriptionHandler>();

        services.AddSingleton<ICommandHandler, StartHandler>();
        services.AddSingleton<ICommandHandler, SetAccessHandler>();
        services.AddSingleton<ICommandHandler, RemoveAccessHandler>();
        services.AddSingleton<ICommandHandler, ClearHistoryHandler>();
        services.AddSingleton<ICommandHandler, ImageCommandHandler>();

        services.AddSingleton<ICallbackHandler, GeneratePhotoHandler>();

        services.AddSingleton<MessagesReceiver>();
        services.AddSingleton<UpdateSystem>();
        services.AddSingleton<TelegramBot>();
        services.AddSingleton<GPTClient>();

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
    
}