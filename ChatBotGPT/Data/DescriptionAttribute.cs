namespace VideoBot.Data;

public class DescriptionAttribute : Attribute
{
    public string Value { get; }

    public DescriptionAttribute(string value)
    {
        Value = value;
    }
}