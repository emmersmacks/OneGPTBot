using Newtonsoft.Json;

namespace VideoBot.Services;

public class ConfigService
{
    private Config _config;
    
    public ConfigService()
    {
        LoadConfig();
    }

    private void LoadConfig()
    {
        var settingsPath = Path.Combine(Directory.GetCurrentDirectory(), "config.json");
        var json = File.ReadAllText(settingsPath);
        Console.WriteLine(json);
        _config = JsonConvert.DeserializeObject<Config>(json) ?? throw new InvalidOperationException("Config file parse is fail");
    }

    public Config GetConfig()
    {
        if (_config == null)
            throw new Exception("Config is null, but you trying to get them");
        return _config;
    }
}