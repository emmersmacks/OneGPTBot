using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using VideoBot.Data;
using VideoBot.Handlers.Messages;
using VideoBot.Services;
using File = System.IO.File;

namespace VideoBot.Handlers;

public class MessagesReceiver
{
    private readonly AccessDataService _accessDataService;
    private readonly IEnumerable<ITextAccessibleHandler> _textHandlers;
    private readonly IEnumerable<ICallbackHandler> _callbackHandlers;
    private readonly IEnumerable<IPhotoAccessibleHandler> _photoHandlers;
    private readonly IEnumerable<ICommandHandler> _commandHandlers;

    public MessagesReceiver(
        AccessDataService accessDataService,
        IEnumerable<ITextAccessibleHandler> textHandlers,
        IEnumerable<ICallbackHandler> callbackHandlers,
        IEnumerable<IPhotoAccessibleHandler> photoHandlers,
        IEnumerable<ICommandHandler> commandHandlers)
    {
        _accessDataService = accessDataService;
        _textHandlers = textHandlers;
        _callbackHandlers = callbackHandlers;
        _photoHandlers = photoHandlers;
        _commandHandlers = commandHandlers;
    }

    public async void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
    {
        Console.WriteLine(DateTime.Now.Ticks.ToString() + " " + messageEventArgs.Message);
        switch (messageEventArgs.Message.Type)
        {
            case MessageType.Text:
                if(messageEventArgs.Message.Text.StartsWith("/"))
                    await HandleMessage(_commandHandlers, messageEventArgs);
                else
                    await HandleMessage(_textHandlers, messageEventArgs);
                break;
            case MessageType.Photo:
                await HandleMessage(_photoHandlers, messageEventArgs);
                break;
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
    
    private async Task HandleMessage(IEnumerable<IAccessibleHandler> handlers, MessageEventArgs messageEventArgs)
    {
        foreach (var handler in handlers)
        {
            if(CheckAccess(handler, messageEventArgs) && CheckCondition(handler, messageEventArgs))
            {
                await handler.Handle(messageEventArgs.Message);
                return;
            }
        }
    }

    private bool CheckCondition(IAccessibleHandler handler, MessageEventArgs messageEventArgs)
    {
        return handler.GetCondition(messageEventArgs.Message);
    }

    private bool CheckAccess(IAccessibleHandler handler, MessageEventArgs messageEventArgs)
    {
        switch (handler.AccessType)
        {
            case EAccessType.Admin:
                return _accessDataService.UserIsAdministrator(messageEventArgs.Message.From.Id);
            case EAccessType.AuthorizedUser:
                return _accessDataService.UserIsAvailable(messageEventArgs.Message.From.Id);
            case EAccessType.UnauthorizedUser:
                return !_accessDataService.UserIsAvailable(messageEventArgs.Message.From.Id);
            default:
                return false;
        }
    }
}