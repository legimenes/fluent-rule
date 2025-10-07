namespace FluentRule.RuleExtensions;
public static class GenericRuleExtensions
{
    public static TSelf Satisfies<T, TProperty, TSelf>(
        this PropertyRule<T, TProperty, TSelf> rule,
        Func<TProperty, bool> predicate,
        object? comparisonValue = null)
        where TSelf : PropertyRule<T, TProperty, TSelf>
    {
        if (rule.ShouldValidate())
        {
            var value = rule.GetValue();
            if (!predicate(value))
            {
                rule.AddNotification(
                    "{PropertyName} não satisfez a condição. Valor atual: {Value} {ComparisonValue}",
                    new Dictionary<string, object?>
                    {
                        ["ComparisonValue"] = comparisonValue
                    });
            }
        }
        return (TSelf)rule;
    }
}