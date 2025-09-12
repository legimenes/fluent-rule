using System.Linq.Expressions;

namespace FluentRule;
public class Contract<T>
{
    public T Instance { get; }
    private readonly List<Notification> _notifications;

    public Contract(T instance)
    {
        Instance = instance;
        _notifications = [];
    }

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    internal void AddNotification(string property, string message)
            => _notifications.Add(new Notification(property, message));

    public StringPropertyRule<T> RuleFor(Expression<Func<T, string>> expression)
    {
        var propertyName = ((MemberExpression)expression.Body).Member.Name;
        return new StringPropertyRule<T>(propertyName, expression.Compile(), this);
    }

    public DecimalPropertyRule<T> RuleFor(Expression<Func<T, int>> expression)
    {
        var propertyName = ((MemberExpression)expression.Body).Member.Name;
        return new DecimalPropertyRule<T>(propertyName, expression.Compile(), this);
    }

    public IDictionary<string, List<string>> GroupedNotifications()
    {
        return _notifications
            .GroupBy(n => n.Key)
            .ToDictionary(g => g.Key, g => g.Select(n => n.Message).ToList());
    }
}
