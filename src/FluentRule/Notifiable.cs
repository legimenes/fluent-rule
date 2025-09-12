namespace FluentRule;
public abstract class Notifiable : IDisposable
{
    private readonly List<Notification> _notifications;

    protected Notifiable() { _notifications = []; }

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    /// <summary>
    /// Add a notification directly with a message
    /// </summary>
    /// <param name="message">Custom error message to use when validation fails</param>
    public void AddNotification(string message)
    {
        _notifications.Add(new Notification(null, message));
    }

    /// <summary>
    /// Add a notification directly with key and message
    /// </summary>
    /// <param name="key">Key representing the context to validate</param>
    /// <param name="message">Custom error message to use when validation fails</param>
    public void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }

    public void AddNotifications(Notifiable notifiableObject)
    {
        AddNotifications(notifiableObject.Notifications);
    }

    public void AddNotifications(IReadOnlyCollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void Clear()
    {
        _notifications.Clear();
    }

    public bool IsValid => _notifications.Count == 0;

    public void Dispose()
    {
        _notifications.Clear();

        GC.SuppressFinalize(this);
    }
}
