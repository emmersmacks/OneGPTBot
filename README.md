# OneGPTBot

Telegram bot written for convenient communication with the chatbot - ChatGPT and neural network for image generation - DALL-E.

## Deploy
To deploy the bot on a Linux server

- Install .Net
```
sudo apt install apt-transport-https ca-certificates
wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt install dotnet-sdk-7.0
```
- Install git and dowload project
```
sudo apt install git
git config --global user.name "you username"
root@s641360:~# git config --global user.email "you email"
cd path/to/your/folder
git clone https://github.com/emmersmacks/OneGPTBot.git
```
- Build and run project
```
dotnet build ChatBotGPT.sln -c Release
cd ChatBotGPT/bin/Release/net7.0
dotnet ChatBotGPT.dll
```
### Attention!!!
It is also necessary to create a config.json file in the folder with ChatBotGPT.dll.
Example content for the config.json file:
```
{
    "telegram_token":"your_bot_telegram_token",
    "gpt_token":"your_gpt_token",
    "database_connection":"Host=your_server_api;Database=your_table_name;Username=your_username;Password=your_password"
}
```

## API
A **Handler** is a class that accepts a message and reacts to it if the message meets the condition. 

To create a handler, it is necessary to inherit the class from one of the interfaces from the IHandler family.

* **ICommandHandler** - Handles commands (messages starting with "/").
* **IPhotoAccessibleHandler** - Handles the processing of photos sent by the user.
* **ITextAccessibleHandler** - Handles the processing of text.
* **ICallbackHandler** - Handles CallbackQuery.

### Methods
* **Handle** - the main logic of the handler.
* **GetCondition** - the condition for triggering this handler.
* **AccessType** - the level of access to this handler.

After creating the handler, it needs to be registered in the container:
```
services.AddSingleton<ITextAccessibleHandler, TextHandler>();
```
### Example
```
public class HelloHandler : ITextAccessibleHandler
{
    private readonly TelegramMessagesService _telegramMessagesService;

    public HelloHandler(TelegramMessagesService telegramMessagesService)
    {
        _telegramMessagesService = telegramMessagesService;
    }

    public async Task Handle(Message message)
    {
        _telegramMessagesService.SendStartMessage(message);
    }

    public bool GetCondition(Message message)
    {
        return message.Text == "Hello";
    }

    public EAccessType AccessType => EAccessType.AuthorizedUser;
}
```
