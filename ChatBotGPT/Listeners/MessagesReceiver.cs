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
    private readonly UsersDataService _usersDataService;
    private readonly IEnumerable<ITextAccessibleHandler> _textHandlers;
    private readonly IEnumerable<ICallbackHandler> _callbackHandlers;
    private readonly IEnumerable<IPhotoAccessibleHandler> _photoHandlers;
    private readonly IEnumerable<ICommandHandler> _commandHandlers;
    private readonly IEnumerable<IPendingHandler> _pendingHandlers;

    public MessagesReceiver(
        AccessDataService accessDataService,
        UsersDataService usersDataService,
        IEnumerable<ITextAccessibleHandler> textHandlers,
        IEnumerable<ICallbackHandler> callbackHandlers,
        IEnumerable<IPhotoAccessibleHandler> photoHandlers,
        IEnumerable<ICommandHandler> commandHandlers,
        IEnumerable<IPendingHandler> pendingHandlers)
    {
        _accessDataService = accessDataService;
        _usersDataService = usersDataService;
        _textHandlers = textHandlers;
        _callbackHandlers = callbackHandlers;
        _photoHandlers = photoHandlers;
        _commandHandlers = commandHandlers;
        _pendingHandlers = pendingHandlers;
    }

    public async void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
    {
        Console.WriteLine(DateTime.Now.Ticks.ToString() + " " + messageEventArgs.Message);
        
        var isHandlerAvailability = CheckHandlerAvailability(messageEventArgs);
        if(isHandlerAvailability)
            return;

        var handlersList = GetHandlersList(messageEventArgs);
        HandleMessage(handlersList, messageEventArgs);
    }

    private bool CheckHandlerAvailability(MessageEventArgs messageEventArgs)
    {
        var user = _usersDataService.GetUser(messageEventArgs.Message.From.Id);
        if (user != null)
        {
            var handlerName = user.Handler;
            if (handlerName != null && handlerName != "")
            {
                var handler = _pendingHandlers.FirstOrDefault(h => h.GetType().FullName == handlerName);
                handler.Handle(messageEventArgs.Message);
                _usersDataService.RemoveUserHandler(messageEventArgs.Message.From.Id);
                return true;
            }
        }

        return false;
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

    private IEnumerable<IAccessibleHandler> GetHandlersList(MessageEventArgs messageEventArgs)
    {
        switch (messageEventArgs.Message.Type)
        {
            case MessageType.Text:
                if (messageEventArgs.Message.Text.StartsWith("/"))
                    return _commandHandlers;
                else
                    return _textHandlers;
            case MessageType.Photo:
                return _photoHandlers;
        }

        return null;
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