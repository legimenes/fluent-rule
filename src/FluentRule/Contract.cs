using System.Linq.Expressions;

namespace FluentNotification;
public class Contract<T>
{
    private readonly T _instance;
    private readonly List<Notification> _notifications;

    public Contract(T instance)
    {
        _instance = instance;
        _notifications = [];
    }

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    internal void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }

    public PropertyRule<T> RuleFor(Expression<Func<T, object>> property)
    {
        return new PropertyRule<T>(this, _instance, property);
    }

    public IDictionary<string, List<string>> GroupedNotifications()
    {
        return _notifications
            .GroupBy(n => n.Key)
            .ToDictionary(g => g.Key, g => g.Select(n => n.Message).ToList());
    }
}
