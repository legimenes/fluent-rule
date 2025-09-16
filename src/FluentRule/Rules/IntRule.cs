namespace FluentRule.Rules;
public class IntRule<T>(
    string propertyName,
    Func<T, int> accessor,
    Contract<T> parent)
    : PropertyRule<T, int, IntRule<T>>(propertyName, accessor, parent)
{
}