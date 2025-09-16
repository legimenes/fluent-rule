using FluentRule.Rules;
using System.Linq.Expressions;

namespace FluentRule;
public class Contract<T>(T instance)
{
    private readonly List<Notification> _notifications = [];

    public T Instance { get; } = instance;

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public DecimalRule<T> RuleFor(Expression<Func<T, decimal>> expression)
    {
        var propertyName = ((MemberExpression)expression.Body).Member.Name;
        return new DecimalRule<T>(propertyName, expression.Compile(), this);
    }

    public IntRule<T> RuleFor(Expression<Func<T, int>> expression)
    {
        var propertyName = ((MemberExpression)expression.Body).Member.Name;
        return new IntRule<T>(propertyName, expression.Compile(), this);
    }

    public StringRule<T> RuleFor(Expression<Func<T, string>> expression)
    {
        var propertyName = ((MemberExpression)expression.Body).Member.Name;
        return new StringRule<T>(propertyName, expression.Compile(), this);
    }

    //public IDictionary<string, List<string>> GroupedNotifications()
    //{
    //    return _notifications
    //        .GroupBy(n => n.Key)
    //        .ToDictionary(g => g.Key, g => g.Select(n => n.Message).ToList());
    //}

    internal void AddNotification(string property, string message)
    {
        _notifications.Add(new Notification(property, message));
    }
}