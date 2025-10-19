using FluentRule.Localization;
using FluentRule.Rules;

namespace FluentRule.RuleExtensions;
public static class StringRuleExtensions
{
    public static StringRule<T> IsEnumName<T>(this StringRule<T> rule, Type enumType, bool caseSensitive = true)
    {
        if (rule.ShouldValidate())
        {
            string value = rule.GetValue();
            if (!string.IsNullOrEmpty(value))
            {
                bool isValid = Enum.TryParse(enumType, value, !caseSensitive, out var _)
                    && Enum.GetNames(enumType)
                           .Any(n => string.Equals(n, value, caseSensitive
                               ? StringComparison.Ordinal
                               : StringComparison.OrdinalIgnoreCase));

                if (!isValid)
                {
                    rule.AddNotification(LocalizedMessages.IsEnumName,
                        new Dictionary<string, object?>
                        {
                            [Constants.PlaceHolders.PropertyName] = rule.GetPropertyName(),
                            [Constants.PlaceHolders.PropertyValue] = value,
                            [Constants.PlaceHolders.EnumType] = enumType.Name
                        });
                }
            }
            else
            {
                rule.AddNotification(LocalizedMessages.IsEnumName,
                    new Dictionary<string, object?>
                    {
                        [Constants.PlaceHolders.PropertyName] = rule.GetPropertyName(),
                        [Constants.PlaceHolders.PropertyValue] = value,
                        [Constants.PlaceHolders.EnumType] = enumType.Name
                    });
            }
        }

        return rule;
    }

    public static StringRule<T> MinimumLength<T>(this StringRule<T> rule, int minimumLength)
    {
        if (rule.ShouldValidate())
        {
            string value = rule.GetValue();
            if (string.IsNullOrEmpty(value) || value.Length < minimumLength)
            {
                rule.AddNotification(LocalizedMessages.MinimumLength,
                    new Dictionary<string, object?>
                    {
                        [Constants.PlaceHolders.PropertyName] = rule.GetPropertyName(),
                        [Constants.PlaceHolders.PropertyValue] = value,
                        [Constants.PlaceHolders.MinimumLength] = minimumLength,
                        [Constants.PlaceHolders.TotalLength] = value?.Length ?? 0
                    });
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
                rule.AddNotification(LocalizedMessages.NotNull,
                    new Dictionary<string, object?>
                    {
                        [Constants.PlaceHolders.PropertyName] = rule.GetPropertyName(),
                        [Constants.PlaceHolders.PropertyValue] = value
                    });
        }
        return rule;
    }

    public static StringRule<T> NotNullOrEmpty<T>(this StringRule<T> rule)
    {
        if (rule.ShouldValidate())
        {
            string value = rule.GetValue();
            if (string.IsNullOrEmpty(value))
                rule.AddNotification(LocalizedMessages.NotNullOrEmpty,
                    new Dictionary<string, object?>
                    {
                        [Constants.PlaceHolders.PropertyName] = rule.GetPropertyName(),
                        [Constants.PlaceHolders.PropertyValue] = value
                    });
        }
        return rule;
    }

    public static StringRule<T> NotNullOrWhiteSpace<T>(this StringRule<T> rule)
    {
        if (rule.ShouldValidate())
        {
            string value = rule.GetValue();
            if (string.IsNullOrWhiteSpace(value))
                rule.AddNotification(LocalizedMessages.NotNullOrWhiteSpace,
                    new Dictionary<string, object?>
                    {
                        [Constants.PlaceHolders.PropertyName] = rule.GetPropertyName(),
                        [Constants.PlaceHolders.PropertyValue] = value
                    });
        }
        return rule;
    }

    //public static StringRule<T> IsEmail<T>(this StringRule<T> rule)
    //{
    //    if (rule.ShouldValidate())
    //    {
    //        var value = rule.GetValue();
    //        if (!string.IsNullOrEmpty(value) &&
    //            !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
    //        {
    //            rule.AddNotification("E-mail inválido");
    //        }
    //    }
    //    return rule;
    //}
}