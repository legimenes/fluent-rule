namespace FluentRule;
public class Notification(string? key, string message)
{
    public string? Key { get => key; }
    public string Message { get; private set; } = message;

    internal void OverrideMessage(string newMessage)
    {
        Message = newMessage;
    }
}