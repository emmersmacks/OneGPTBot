using ChatBotGPT.ChatGPT;

namespace BotTest;

public class GptClientTest
{
    [Test]
    public void TestGptConnection()
    {
        var gptClient = new GPTClient();
        var responseType = gptClient.SendMessage("Hello!", new List<string>());
        Assert.NotNull(responseType);
        Assert.NotNull(responseType.Result);
        Assert.NotNull(responseType.Result.choices);
        Assert.IsNotEmpty(responseType.Result.choices);
    }
}