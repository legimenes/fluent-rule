using FluentRule;
using System.Text.RegularExpressions;

namespace FluentRule;
public class StringPropertyRule<T> : PropertyRule<T, string, StringPropertyRule<T>>
{
    public StringPropertyRule(string propertyName, Func<T, string> accessor, Contract<T> parent)
        : base(propertyName, accessor, parent) { }

    public StringPropertyRule<T> IsNotNullOrEmpty(string message)
    {
        if (ShouldValidate())
        {
            var value = _accessor(_parent.Instance);
            if (string.IsNullOrEmpty(value))
                _parent.AddNotification(_propertyName, message);
        }
        return this;
    }

    public StringPropertyRule<T> HasMinLength(int minLength, string message)
    {
        if (ShouldValidate())
        {
            var value = _accessor(_parent.Instance);
            if (string.IsNullOrEmpty(value) || value.Length < minLength)
                _parent.AddNotification(_propertyName, message);
        }
        return this;
    }

    public StringPropertyRule<T> IsEmail(string message)
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
