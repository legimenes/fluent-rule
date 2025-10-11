using FluentRule.Localization;

namespace FluentRule.RuleExtensions;
public static class GenericRuleExtensions
{
    //TODO: Satisfies for Specification Pattern

    public static TSelf Must<T, TProperty, TSelf>(this PropertyRule<T, TProperty, TSelf> rule, Func<TProperty, bool> predicate)
        where TSelf : PropertyRule<T, TProperty, TSelf>
    {
        if (rule.ShouldValidate())
        {
            TProperty? value = rule.GetValue();
            if (!predicate(value))
            {
                rule.AddNotification(Messages.Must,
                    new Dictionary<string, object?>
                    {
                        [Constants.PlaceHolders.PropertyName] = rule.GetPropertyName(),
                        [Constants.PlaceHolders.PropertyValue] = value
                    });
            }
        }
        return (TSelf)rule;
    }
}