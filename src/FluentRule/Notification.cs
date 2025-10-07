namespace FluentRule;
public class Notification
{
    public string? Key { get; }
    public object? Value { get; }
    
    private string RawMessage { get; set; }
    private readonly Dictionary<string, object?> _placeholders;

    public string Message => FormatMessage();

    public Notification(string? key, string message, object? value = null, Dictionary<string, object?>? placeholders = null)
    {
        Key = key;
        RawMessage = message;
        Value = value;
        _placeholders = placeholders ?? [];
    }

    internal void OverrideMessage(string newMessage) => RawMessage = newMessage;

    private string FormatMessage()
    {
        var result = RawMessage
            .Replace("{PropertyName}", Key ?? string.Empty)
            .Replace("{Value}", Value?.ToString() ?? string.Empty);

        foreach (var kv in _placeholders)
            result = result.Replace("{" + kv.Key + "}", kv.Value?.ToString() ?? string.Empty);

        return result;
    }
}