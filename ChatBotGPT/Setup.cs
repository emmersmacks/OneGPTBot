using ChatBotGPT.ChatGPT;
using ChatBotGPT.Database;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using VideoBot.Handlers;
using VideoBot.Handlers.Messages.Impl;
using VideoBot.Services;

namespace ChatBotGPT;

public class Setup
{
    public ServiceProvider Init()
    {
        var services = new ServiceCollection();

        services.AddSingleton(new TelegramBotClient("6118348079:AAGaRzbihjeanrXklNfecWQtPXi4ud6pVZ4"));
        services.AddSingleton(new GPTClient());

        services.AddSingleton<ApplicationContext>();
        services.AddSingleton<TelegramMessagesService>();
        services.AddSingleton<UsersDataService>();
        services.AddSingleton<AccessDataService>();

        services.AddSingleton<IMessageHandler, StartHandler>();
        services.AddSingleton<IMessageHandler, UnauthorizedMessageHandler>();
        services.AddSingleton<IMessageHandler, SetAccessHandler>();
        services.AddSingleton<IMessageHandler, RemoveAccessHandler>();
        services.AddSingleton<IMessageHandler, ClearHistoryHandler>();
        services.AddSingleton<IMessageHandler, PictureHandler>();
        services.AddSingleton<IMessageHandler, MessageHandler>();

        //services.AddSingleton<ICallbackHandler, AcceptAgreementHandler>();

        services.AddSingleton<MessagesReceiver>();
        services.AddSingleton<UpdateSystem>();
        services.AddSingleton<TelegramBot>();

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
    
}