namespace FluentRule.RuleExtensions;
public static class GenericRuleExtensions
{
    public static TSelf Satisfies<T, TProperty, TSelf>(
        this PropertyRule<T, TProperty, TSelf> rule,
        Func<TProperty, bool> predicate)
        where TSelf : PropertyRule<T, TProperty, TSelf>
    {
        if (rule.ShouldValidate())
        {
            var value = rule.GetValue();
            if (!predicate(value))
                rule.AddNotification("A condição não foi satisfeita.");
        }
        return (TSelf)rule;
    }
}