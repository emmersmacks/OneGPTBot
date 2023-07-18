using ChatBotGPT;
using Microsoft.Extensions.DependencyInjection;
using VideoBot.Services;

namespace BotTest;

public class AccessDataServiceTest
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
        var dataService = _serviceProvider.GetService(typeof(AccessDataService)) as AccessDataService;
        var testId = 1313131313;
        
        dataService.AddAccess(testId);
        var accessCreatedLocal = dataService.UserIsAvailable(testId);
        Assert.IsTrue(accessCreatedLocal);
        
        dataService.RemoveAccess(testId);
        accessCreatedLocal = dataService.UserIsAvailable(testId);
        Assert.IsFalse(accessCreatedLocal);
    }
}