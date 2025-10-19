namespace FluentRule;

/// <summary>
/// Represents a validation notification that stores contextual information
/// such as the property key, its value, and a formatted error message.
/// </summary>
public class Notification
{
    /// <summary>
    /// Gets the key (in most cases, property name) associated with the notification, if any.
    /// </summary>
    public string? Key { get; }

    /// <summary>
    /// Gets the value that triggered the notification, if applicable.
    /// </summary>
    public object? Value { get; }

    /// <summary>
    /// Gets the formatted message describing the validation issue.
    /// </summary>
    public string Message { get => _message; }

    /// <summary>
    /// Gets a read-only collection of placeholder values used for message formatting.
    /// </summary>
    public IReadOnlyDictionary<string, object?> Placeholders { get => _placeholders; }

    private string _message = string.Empty;
    private readonly IReadOnlyDictionary<string, object?> _placeholders;

    /// <summary>
    /// Initializes a new instance of the <see cref="Notification"/> class with a message,
    /// an optional key, value, and placeholder dictionary for message formatting.
    /// </summary>
    /// <param name="message">The error or validation message to display.</param>
    /// <param name="key">The property key or identifier related to the notification.</param>
    /// <param name="value">The value that caused the validation failure.</param>
    /// <param name="placeholders">Optional placeholder values for message formatting.</param>
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

    /// <summary>
    /// Replaces the existing message with a new one and applies formatting.
    /// </summary>
    /// <param name="newMessage">The new message template to apply.</param>
    internal void OverrideMessage(string newMessage)
    {
        _message = FormatMessage(newMessage);
    }

    /// <summary>
    /// Applies formatting to the message by replacing placeholders with their corresponding values.
    /// </summary>
    /// <param name="message">The message template containing placeholders.</param>
    /// <returns>A formatted string with placeholder values replaced.</returns>
    private string FormatMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return string.Empty;

        string formattedMessage = message
            .Replace(Constants.PlaceHolders.Key, Key ?? string.Empty)
            .Replace(Constants.PlaceHolders.Value, Value?.ToString() ?? string.Empty);

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