using System.Linq.Expressions;

namespace FluentRule;
public class PropertyRule<T, TProperty, TSelf>
    where TSelf : PropertyRule<T, TProperty, TSelf>
{
    protected readonly string _propertyName;
    protected readonly Func<T, TProperty> _accessor;
    protected readonly Contract<T> _parent;
    protected Func<T, bool>? _condition;

    public PropertyRule(string propertyName, Func<T, TProperty> accessor, Contract<T> parent)
    {
        _propertyName = propertyName;
        _accessor = accessor;
        _parent = parent;
    }

    protected bool ShouldValidate()
        => _condition == null || _condition(_parent.Instance);

    public TSelf When(Expression<Func<T, bool>> condition)
    {
        _condition = condition.Compile();
        return (TSelf)this; // mantém o tipo concreto
    }

    private static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression)
    {
        Expression body = expression.Body;

        // trata conversões (ex.: object boxing)
        if (body is UnaryExpression unary && unary.Operand is MemberExpression)
            body = unary.Operand;

        if (body is MemberExpression member)
            return member.Member.Name;

        throw new InvalidOperationException("Não foi possível obter o nome da propriedade da expressão informada.");
    }

    private static object GetPropertyValue<T>(T instance, Expression<Func<T, object>> expression)
    {
        var func = expression.Compile();
        return func(instance);
    }
}
