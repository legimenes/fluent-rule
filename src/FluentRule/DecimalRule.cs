namespace FluentRule;
public class DecimalRule<T>(
    string propertyName,
    Func<T, int> accessor,
    Contract<T> parent)
    : PropertyRule<T, int, DecimalRule<T>>(propertyName, accessor, parent)
{
    public DecimalRule<T> IsGreaterThan(decimal min, string message)
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