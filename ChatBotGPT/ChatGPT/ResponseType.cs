namespace ChatBotGPT.ChatGPT;

public class ResponseType
{
    public string id;
    public string created;
    public string model;
    public Usage usage;
    public Choice[] choices;
}

public class Usage
{
    public string prompt_tokens;
    public string completion_tokens;
    public string total_tokens;
}

public class Choice
{
    public MessageType message;
    public string finish_reason;
    public int index;
}