using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using VideoBot.Handlers.Messages;
using VideoBot.Services;
using File = System.IO.File;

namespace VideoBot.Handlers;

public class MessagesReceiver
{
    private readonly IEnumerable<IMessageHandler> _messageHandlers;
    private readonly IEnumerable<ICallbackHandler> _callbackHandlers;

    public MessagesReceiver(
        IEnumerable<IMessageHandler> messageHandlers,
        IEnumerable<ICallbackHandler> callbackHandlers)
    {
        _messageHandlers = messageHandlers;
        _callbackHandlers = callbackHandlers;
    }

    public async void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
    {
        Console.WriteLine(DateTime.Now.Ticks.ToString() + " " + messageEventArgs.Message);
        foreach (var handler in _messageHandlers)
        {
            if (handler.GetCondition(messageEventArgs.Message))
            {
                await handler.Handle(messageEventArgs.Message);
                return;
            }
        }
    }
    
    public async void OnCallbackReceived(object sender, CallbackQueryEventArgs messageEventArgs)
    {
        Console.WriteLine(DateTime.Now.Ticks.ToString() + " " + messageEventArgs.CallbackQuery.Message);
        foreach (var handler in _callbackHandlers)
        {
            if (handler.GetCondition(messageEventArgs.CallbackQuery))
            {
                await handler.Handle(messageEventArgs.CallbackQuery);
                return;
            }
        }
    }
}