﻿using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using VideoBot.Data;
using VideoBot.Services;

namespace VideoBot.Handlers.Messages.Impl;

public class ClearHistoryHandler : ICommandHandler
{
    private readonly UsersDataService _usersDataService;
    private readonly AccessDataService _accessDataService;
    private readonly TelegramMessagesService _telegramMessagesService;

    public ClearHistoryHandler(
        UsersDataService usersDataService,
        AccessDataService accessDataService,
        TelegramMessagesService telegramMessagesService)
    {
        _usersDataService = usersDataService;
        _accessDataService = accessDataService;
        _telegramMessagesService = telegramMessagesService;
    }

    public EAccessType AccessType => EAccessType.AuthorizedUser;

    public async Task Handle(Message message)
    {
        var user = _usersDataService.GetUser(message.From.Id);
        user.Messages = new List<string>();
        _usersDataService.UpdateUser(user);

        _telegramMessagesService.SendHistoryClearMessage(message);
        
    }

    public bool GetCondition(Message message)
    {
        return message.Text.StartsWith("/clear_history");
    }
}