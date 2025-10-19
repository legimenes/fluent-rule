using FluentRule.Localization;

namespace FluentRule.RuleExtensions;
public static class GenericRuleExtensions
{
    //TODO: Satisfies for Specification Pattern

    public static TSelf Must<T, TProperty, TSelf>(this PropertyRule<T, TProperty, TSelf> rule, Func<TProperty, bool> predicate)
        where TSelf : PropertyRule<T, TProperty, TSelf>
    {
        if (!rule.ShouldValidate())
            return (TSelf)rule;

        TProperty? value = rule.GetValue();
        if (!predicate(value))
        {
            rule.AddNotification(LocalizedMessages.Must,
                new Dictionary<string, object?>
                {
                    [Constants.PlaceHolders.Key] = rule.GetPropertyName(),
                    [Constants.PlaceHolders.Value] = value
                });
        }
        return (TSelf)rule;
    }

    public static TSelf Custom<T, TProperty, TSelf>(
        this PropertyRule<T, TProperty, TSelf> rule,
        Action<TProperty?, ValidationContext<T>> customAction)
        where TSelf : PropertyRule<T, TProperty, TSelf>
    {
        if (!rule.ShouldValidate())
            return (TSelf)rule;

        TProperty? value = rule.GetValue();
        T instance = rule.ParentInstance!;

        ValidationContext<T> ctx = new (instance, (message, metadata) =>
        {
            if (metadata == null)
            {
                metadata = new Dictionary<string, object?>()
                {
                    [Constants.PlaceHolders.Key] = rule.GetPropertyName(),
                    [Constants.PlaceHolders.Value] = value
                };
            }
            else if (!metadata.ContainsKey(Constants.PlaceHolders.Key))
                metadata[Constants.PlaceHolders.Key] = rule.GetPropertyName();

            rule.AddNotification(message, metadata);
        });

        try
        {
            customAction(value, ctx);
        }
        catch (Exception ex)
        {
            rule.AddNotification(LocalizedMessages.CustomError, new Dictionary<string, object?>
            {
                [Constants.PlaceHolders.CustomError] = ex.Message,
                [Constants.PlaceHolders.Key] = rule.GetPropertyName(),
                [Constants.PlaceHolders.Value] = value
            });
            return (TSelf)rule;
        }
        return (TSelf)rule;
    }
}