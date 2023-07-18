using ChatBotGPT.ChatGPT;

namespace VideoBot;

public class Error
{
    public ErrorType error;
}

public class ErrorType
{
    public string message;
    public string type;
    public string param;
    public string code;
}