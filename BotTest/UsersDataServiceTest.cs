using ChatBotGPT;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Telegram.Bot.Types;
using VideoBot.Services;
using Message = Telegram.Bot.Types.Message;

namespace BotTest;

public class UsersDataServiceTest
{
    private ServiceProvider _serviceProvider;
    
    [SetUp]
    public void Setup()
    {
        var setup = new Setup();
        _serviceProvider = setup.Init();
    }
    
    [Test]
    public void TestAccessDataService()
    {
        var dataService = _serviceProvider.GetService(typeof(UsersDataService)) as UsersDataService;
        var testId = 1313131313;
        
        var message = new Message();
        message.From = new User();
        message.From.Id = testId;
        message.From.Username = "Test";
        message.From.FirstName = "Test";
        message.From.LastName = "Test";
        dataService.CreateDefaultUser(message);
        
        var accessCreatedLocal = dataService.GetUser(testId);
        Assert.IsNotNull(accessCreatedLocal);
        
        dataService.DeleteUser(testId);
        accessCreatedLocal = dataService.GetUser(testId);
        Assert.IsNull(accessCreatedLocal);
    }
}