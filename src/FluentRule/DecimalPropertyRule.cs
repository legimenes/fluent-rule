using FluentRule;

namespace FluentRule;
public class DecimalPropertyRule<T> : PropertyRule<T, int, DecimalPropertyRule<T>>
{
    public DecimalPropertyRule(string propertyName, Func<T, int> accessor, Contract<T> parent)
        : base(propertyName, accessor, parent) { }

    public DecimalPropertyRule<T> IsGreaterThan(decimal min, string message)
    {
        if (ShouldValidate())
        {
            var value = _accessor(_parent.Instance);
            if (value <= min)
                _parent.AddNotification(_propertyName, message);
        }
        return this;
    }
}
