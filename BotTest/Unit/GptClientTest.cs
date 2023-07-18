using ChatBotGPT.ChatGPT;
using VideoBot.Services;

namespace BotTest;

public class GptClientTest
{
    [Test]
    public void TestGptConnection()
    {
        var gptClient = new GPTClient(new ConfigService());
        var responseType = gptClient.SendMessage("Hello!", new List<string>());
        Assert.NotNull(responseType);
        Assert.NotNull(responseType.Result);
    }
}