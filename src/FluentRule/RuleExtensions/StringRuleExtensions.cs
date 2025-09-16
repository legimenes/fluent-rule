using FluentRule.Rules;
using System.Text.RegularExpressions;

namespace FluentRule.RuleExtensions;
public static class StringRuleExtensions
{
    public static StringRule<T> HasMinLength<T>(this StringRule<T> rule, int minLength, string message)
    {
        if (rule.ShouldValidate())
        {
            var value = rule.GetValue();
            if (string.IsNullOrEmpty(value) || value.Length < minLength)
                rule.AddNotification(message);
        }
        return rule;
    }

    public static StringRule<T> IsEmail<T>(this StringRule<T> rule, string message)
    {
        if (rule.ShouldValidate())
        {
            var value = rule.GetValue();
            if (!string.IsNullOrEmpty(value) &&
                !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                rule.AddNotification(message);
            }
        }
        return rule;
    }

    public static StringRule<T> NotNull<T>(this StringRule<T> rule, string message)
    {
        if (rule.ShouldValidate())
        {
            string value = rule.GetValue();
            if (value is null)
                rule.AddNotification(message);
        }
        return rule;
    }

    public static StringRule<T> NotNullOrEmpty<T>(this StringRule<T> rule, string message)
    {
        if (rule.ShouldValidate())
        {
            string value = rule.GetValue();
            if (string.IsNullOrEmpty(value))
                rule.AddNotification(message);
        }
        return rule;
    }

    public static StringRule<T> NotNullOrWhitespace<T>(this StringRule<T> rule, string message)
    {
        if (rule.ShouldValidate())
        {
            string value = rule.GetValue();
            if (string.IsNullOrWhiteSpace(value))
                rule.AddNotification(message);
        }
        return rule;
    }

    //public static StringRule<T> IsCpf<T>(
    //    this StringRule<T> rule, string message)
    //{
    //    if (rule.ShouldValidate())
    //    {
    //        var value = rule.GetValue();
    //        if (!CpfValidator.IsValid(value)) // aqui você pode plugar sua lógica de CPF
    //            rule.AddNotification(message);
    //    }
    //    return rule;
    //}
}
