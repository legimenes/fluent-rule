using System.Text.RegularExpressions;

namespace FluentRule;
public class StringRule<T>(
    string propertyName,
    Func<T, string> accessor,
    Contract<T> parent)
    : PropertyRule<T, string, StringRule<T>>(propertyName, accessor, parent)
{
    public StringRule<T> IsNotNullOrEmpty(string message)
    {
        if (ShouldValidate())
        {
            var value = _accessor(_parent.Instance);
            if (string.IsNullOrEmpty(value))
                _parent.AddNotification(_propertyName, message);
        }
        return this;
    }

    public StringRule<T> HasMinLength(int minLength, string message)
    {
        if (ShouldValidate())
        {
            var value = _accessor(_parent.Instance);
            if (string.IsNullOrEmpty(value) || value.Length < minLength)
                _parent.AddNotification(_propertyName, message);
        }
        return this;
    }

    public StringRule<T> IsEmail(string message)
    {
        if (ShouldValidate())
        {
            var value = _accessor(_parent.Instance);
            if (!string.IsNullOrEmpty(value) &&
                !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                _parent.AddNotification(_propertyName, message);
            }
        }
        return this;
    }
}