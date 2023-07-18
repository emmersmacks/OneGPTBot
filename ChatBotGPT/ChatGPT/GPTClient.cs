using System.Net;
using Newtonsoft.Json;
using Telegram.Bot.Types.InputFiles;
using VideoBot;
using VideoBot.Data;
using VideoBot.Services;

namespace ChatBotGPT.ChatGPT;

public class GPTClient
{
    private readonly ConfigService _configService;
    private readonly string _model;
    private readonly string _apiKey;
    
    private const string CHAT_URL = "https://api.openai.com/v1/chat/completions";
    private const string DALLE_URL = "https://api.openai.com/v1/images/generations";
    
    public GPTClient(ConfigService configService)
    {
        _configService = configService;
        _apiKey = _configService.GetString(ConfigNames.GptToken);
        _model = "gpt-3.5-turbo";
    }

    public async Task<Response> SendMessage(string text, List<string> messagesDict)
    {
        var messages = new List<MessageType>();
        foreach (var messagePair in messagesDict)
        {
            var role = messagePair.Split(":")[0];
            var message = messagePair.Substring(role.Length + 1);
            
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
            max_tokens = 1000,
        };
        
        using (var httpClient = new HttpClient()) {
            
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            var requestJson = JsonConvert.SerializeObject(request);
            var requestContent = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json");
            var httpResponseMessage = await httpClient.PostAsync(CHAT_URL, requestContent);
            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            
            Console.WriteLine(jsonString);

            var response = new Response();
            
            response.Message = JsonConvert.DeserializeObject<Message>(jsonString);
            response.Error = JsonConvert.DeserializeObject<Error>(jsonString);
            
            return response;
        }
    }

    public async Task<InputOnlineFile> GeneratePhoto(string prompt, int count)
    {
        var request = new
        {
            prompt = prompt,
            n = 1,
            size = "1024x1024"
        };

        using (var httpClient = new HttpClient()) {
            
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            var requestJson = JsonConvert.SerializeObject(request);
            var requestContent = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json");
            var httpResponseMessage = httpClient.PostAsync(DALLE_URL, requestContent).Result;
            var responseText = httpResponseMessage.Content.ReadAsStringAsync();
            if (responseText.Result.Contains("error"))
            {
                return null;
            }
            else
            {
                var response = JsonConvert.DeserializeObject<Responce>(responseText.Result);
                using (var httpClientPhoto = new HttpClient())
                {
                    var responseImage = await httpClientPhoto.GetAsync(response.Data[0].Url);
                    var imageStream = await responseImage.Content.ReadAsStreamAsync();
                    var file = new InputOnlineFile(imageStream, "photo.png");
                    return file;
                }
            }
            
        }
    }
}

public class Responce
{
    public long Created;
    public List<Data> Data;
}

public class Data
{
    public string Url;
}