using System.Linq.Expressions;

namespace FluentRule;
public class PropertyRule<T, TProperty, TSelf>(
    string propertyName,
    Func<T, TProperty> accessor,
    Contract<T> parent)
    where TSelf : PropertyRule<T, TProperty, TSelf>
{
    protected readonly string _propertyName = propertyName;
    protected readonly Func<T, TProperty> _accessor = accessor;
    protected readonly Contract<T> _parent = parent;
    protected Func<T, bool>? _condition;

    public TSelf Satisfies(Func<TProperty, bool> predicate, string message)
    {
        if (ShouldValidate())
        {
            var value = _accessor(_parent.Instance);
            if (!predicate(value))
                _parent.AddNotification(_propertyName, message);
        }
        return (TSelf)this;
    }

    public TSelf When(Expression<Func<T, bool>> condition)
    {
        _condition = condition.Compile();
        return (TSelf)this;
    }

    protected bool ShouldValidate()
    {
        return _condition == null || _condition(_parent.Instance);
    }

    //private static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression)
    //{
    //    Expression body = expression.Body;

    //    if (body is UnaryExpression unary && unary.Operand is MemberExpression)
    //        body = unary.Operand;

    //    if (body is MemberExpression member)
    //        return member.Member.Name;

    //    throw new InvalidOperationException("Não foi possível obter o nome da propriedade da expressão informada.");
    //}

    //private static object GetPropertyValue<T>(T instance, Expression<Func<T, object>> expression)
    //{
    //    var func = expression.Compile();
    //    return func(instance);
    //}
}