using Newtonsoft.Json;

namespace ChatBotGPT.ChatGPT;

public class GPTClient
{
    private readonly string _model;
    
    private const string API_KEY = "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";
    private const string CHAT_URL = "https://api.openai.com/v1/chat/completions";
    
    public GPTClient()
    {
        _model = "gpt-3.5-turbo";
    }

    public async Task<ResponseType> SendMessage(string text, List<string> messagesDict)
    {
        var messages = new List<MessageType>();
        foreach (var messagePair in messagesDict)
        {
            var role = messagePair.Split(":")[0];
            var message = messagePair.Substring(role.Length);
            
            var messageType = new MessageType()
            {
                role = role,
                content = message
            };
            messages.Add(messageType);
        }
        
        var newMessage = new MessageType() { role = "user", content = text };
        messages.Add(newMessage);
        
        var request = new
        {
            messages = messages,
            model = _model,
            max_tokens = 300,
        };
        
        using (var httpClient = new HttpClient()) {
            
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");
            var requestJson = JsonConvert.SerializeObject(request);
            var requestContent = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json");
            var httpResponseMessage = httpClient.PostAsync(CHAT_URL, requestContent).Result;
            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            var responseObject = JsonConvert.DeserializeObject<ResponseType>(jsonString);
            return responseObject;
        }
    }
}