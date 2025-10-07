using FluentRule.Rules;
using System.Text.RegularExpressions;

namespace FluentRule.RuleExtensions;
public static class StringRuleExtensions
{
    //public static StringRule<T> HasMinLength<T>(this StringRule<T> rule, int minLength)
    //{
    //    if (rule.ShouldValidate())
    //    {
    //        var value = rule.GetValue();
    //        if (string.IsNullOrEmpty(value) || value.Length < minLength)
    //            rule.AddNotification("Valor mínimo inválido");
    //    }
    //    return rule;
    //}

    public static StringRule<T> HasMinLength<T>(this StringRule<T> rule, int minLength)
    {
        if (rule.ShouldValidate())
        {
            var value = rule.GetValue();
            if (string.IsNullOrEmpty(value) || value.Length < minLength)
            {
                rule.AddNotification(
                    "{PropertyName} deve ter pelo menos {MinLength} caracteres. Valor atual: {Value} (tamanho: {TotalLength})",
                    new Dictionary<string, object?>
                    {
                        ["MinLength"] = minLength,
                        ["TotalLength"] = value?.Length ?? 0
                    });
            }
        }
        return rule;
    }

    public static StringRule<T> IsEmail<T>(this StringRule<T> rule)
    {
        if (rule.ShouldValidate())
        {
            var value = rule.GetValue();
            if (!string.IsNullOrEmpty(value) &&
                !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                rule.AddNotification("E-mail inválido");
            }
        }
        return rule;
    }

    public static StringRule<T> NotNull<T>(this StringRule<T> rule)
    {
        if (rule.ShouldValidate())
        {
            string value = rule.GetValue();
            if (value is null)
                rule.AddNotification("O valor não pode ser nulo.");
        }
        return rule;
    }

    //public static StringRule<T> NotNullOrEmpty<T>(this StringRule<T> rule)
    //{
    //    if (rule.ShouldValidate())
    //    {
    //        string value = rule.GetValue();
    //        if (string.IsNullOrEmpty(value))
    //            rule.AddNotification("O valor não pode ser nulo ou vazio.");
    //    }
    //    return rule;
    //}

    public static StringRule<T> NotNullOrEmpty<T>(this StringRule<T> rule)
    {
        if (rule.ShouldValidate())
        {
            var value = rule.GetValue();
            if (string.IsNullOrEmpty(value))
                rule.AddNotification("{PropertyName} não pode ser nulo ou vazio. Valor atual: {Value}");
        }
        return rule;
    }

    public static StringRule<T> NotNullOrWhiteSpace<T>(this StringRule<T> rule)
    {
        if (rule.ShouldValidate())
        {
            string value = rule.GetValue();
            if (string.IsNullOrWhiteSpace(value))
                rule.AddNotification("O valor não pode ser nulo ou com espaços em branco.");
        }
        return rule;
    }
}
