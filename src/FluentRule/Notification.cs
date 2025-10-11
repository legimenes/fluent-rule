namespace FluentRule;
public class Notification
{
    public string? Key { get; }
    public object? Value { get; }    
    public string Message { get => _message; }
    public IReadOnlyDictionary<string, object?> PlaceHolders { get => _placeholders; }

    private string _message = string.Empty;
    private readonly IReadOnlyDictionary<string, object?> _placeholders;

    public Notification(
        string message,
        string? key = null,
        object? value = null,
        IDictionary<string, object?>? placeholders = null)
    {
        Key = key;
        Value = value;
        _placeholders = new Dictionary<string, object?>(placeholders ?? new Dictionary<string, object?>());
        _message = FormatMessage(message);
    }

    internal void OverrideMessage(string newMessage)
    {
        _message = FormatMessage(newMessage);
    }

    private string FormatMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return string.Empty;

        string formattedMessage = message
            .Replace("{PropertyName}", Key ?? string.Empty)
            .Replace("{Value}", Value?.ToString() ?? string.Empty);

        foreach (var (placeholder, val) in _placeholders)
        {
            formattedMessage = formattedMessage.Replace(
                "{" + placeholder + "}",
                val?.ToString() ?? string.Empty,
                StringComparison.OrdinalIgnoreCase
            );
        }

        return formattedMessage;
    }
}