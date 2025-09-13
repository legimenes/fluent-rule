namespace FluentRule;
public class IntRule<T>(
    string propertyName,
    Func<T, int> accessor,
    Contract<T> parent)
    : PropertyRule<T, int, IntRule<T>>(propertyName, accessor, parent)
{
    public IntRule<T> IsGreaterThan(int min, string message)
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