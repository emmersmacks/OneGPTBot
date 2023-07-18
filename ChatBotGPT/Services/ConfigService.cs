using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideoBot.Data;

namespace VideoBot.Services;

public class ConfigService
{
    private JObject _configuration;
    
    public ConfigService()
    {
        using (var reader = new StreamReader("config.json"))
        {
            var json = reader.ReadToEnd();
            _configuration = JObject.Parse(json);
        }
    }

    public string GetString(string key)
    {
        return _configuration.GetValue(key).ToString();
    }
}