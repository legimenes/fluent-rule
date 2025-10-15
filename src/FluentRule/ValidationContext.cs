namespace FluentRule;
public class ValidationContext<T>(T instance, Action<string, Dictionary<string, object?>?> addNotification)
{
    public T Instance { get; } = instance;

    private readonly Action<string, Dictionary<string, object?>?> _addNotification = addNotification;

    public void AddNotification(string message, Dictionary<string, object?>? metadata = null)
    {
        _addNotification(message, metadata);
    }
}