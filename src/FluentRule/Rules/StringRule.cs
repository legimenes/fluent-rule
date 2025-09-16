using System.Text.RegularExpressions;

namespace FluentRule.Rules;
public class StringRule<T>(
    string propertyName,
    Func<T, string> accessor,
    Contract<T> parent)
    : PropertyRule<T, string, StringRule<T>>(propertyName, accessor, parent)
{
}