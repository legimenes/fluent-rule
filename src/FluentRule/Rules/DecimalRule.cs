namespace FluentRule.Rules;
public class DecimalRule<T>(
    string propertyName,
    Func<T, decimal> accessor,
    Contract<T> parent)
    : PropertyRule<T, decimal, DecimalRule<T>>(propertyName, accessor, parent)
{
}