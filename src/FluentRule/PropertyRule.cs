using System.Linq.Expressions;

namespace FluentRule;
public class PropertyRule<T, TProperty, TSelf>(
    string propertyName,
    Func<T, TProperty> accessor,
    Contract<T> parent)
    where TSelf : PropertyRule<T, TProperty, TSelf>
{
    protected readonly string _propertyName = propertyName;
    protected readonly Func<T, TProperty> _accessor = accessor;
    protected readonly Contract<T> _parent = parent;
    protected Func<T, bool>? _condition;

    private Notification? _lastNotification;

    public TSelf When(Expression<Func<T, bool>> condition)
    {
        _condition = condition.Compile();
        return (TSelf)this;
    }

    public bool ShouldValidate()
    {
        return _condition == null || _condition(_parent.Instance);
    }

    public TProperty GetValue()
    {
        return _accessor(_parent.Instance);
    }

    public TSelf WithMessage(string message)
    {
        if (_lastNotification is not null)
        {
            _lastNotification.OverrideMessage(message);
        }
        return (TSelf)this;
    }

    public void AddNotification(string defaultMessage)
    {
        _lastNotification = new Notification(_propertyName, defaultMessage);
        _parent.AddNotification(_lastNotification);
    }
}