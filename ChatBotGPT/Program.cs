// See https://aka.ms/new-console-template for more information

using ChatBotGPT;
using ChatBotGPT.ChatGPT;
using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using VideoBot.Data.SSH;
using VideoBot.Handlers;
using VideoBot.Handlers.Messages.Impl;
using VideoBot.Services;

var setup = new Setup();
var serviceProvider = setup.Init();
var path = Directory.GetCurrentDirectory();
var bot = serviceProvider.GetService<TelegramBot>();
await bot.Start();

Console.WriteLine("Program ended");


