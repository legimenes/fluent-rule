using FluentRule.Rules;

namespace FluentRule.Tests.Extensions;
public static class CustomRuleExtensions
{
    public static StringRule<T> IsCpf<T>(this StringRule<T> rule, string message)
    {
        if (rule.ShouldValidate())
        {
            var value = rule.GetValue();
            //if (!CpfValidator.IsValid(value))
            if (value.Length > 11)
                rule.AddNotification(message);
        }
        return rule;
    }
}