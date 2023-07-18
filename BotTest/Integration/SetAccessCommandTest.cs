using ChatBotGPT;
using Microsoft.Extensions.DependencyInjection;

namespace BotTest;

public class SetAccessCommandTest
{
    private ServiceProvider _serviceProvider;
    
    [SetUp]
    public void Setup()
    {
        var setup = new Setup();
        _serviceProvider = setup.Init();
    }
    
    //[Test]
    //public async void TestAddAccessCommand()
    //{
    //    var handler = _serviceProvider.GetService(typeof(SetAccessHandler)) as SetAccessHandler;
    //    var message = new Message()
    //    {
    //        Text = "/set_access"
    //    };
    //    var condition = handler.GetCondition(message);
    //    Assert.IsTrue(condition);
    //    await handler.Handle(message);
    //}
}