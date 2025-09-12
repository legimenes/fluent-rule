using System.Linq.Expressions;

namespace FluentNotification;
public class PropertyRule<T>
{
    private readonly Contract<T> _parent;
    private readonly T _instance;
    private readonly string _propertyName;
    private readonly Func<T, object> _accessor;
    private Func<T, bool> _condition;

    public PropertyRule(Contract<T> parent, T instance, Expression<Func<T, object>> property)
    {
        _parent = parent;
        _instance = instance;
        _propertyName = GetPropertyName(property);
        _accessor = property.Compile();
        _condition = _ => true; // padrão: sempre validar
    }

    private object Value => _accessor(_instance);

    private bool ShouldValidate() => _condition(_instance);

    public PropertyRule<T> When(Func<T, bool> condition)
    {
        _condition = condition;
        return this;
    }

    public PropertyRule<T> IsNotNullOrEmpty(string message)
    {
        if (ShouldValidate())
        {
            if (Value is not string str || string.IsNullOrWhiteSpace(str))
                _parent.AddNotification(_propertyName, message);
        }
        return this;
    }

    public PropertyRule<T> IsEmail(string message)
    {
        if (ShouldValidate())
        {
            if (Value is not string str || string.IsNullOrWhiteSpace(str) || !str.Contains("@"))
                _parent.AddNotification(_propertyName, message);
        }
        return this;
    }

    public PropertyRule<T> IsGreaterThan(decimal comparer, string message)
    {
        if (ShouldValidate())
        {
            if (Value is not decimal dec || dec <= comparer)
                _parent.AddNotification(_propertyName, message);
        }
        return this;
    }

    public PropertyRule<T> HasMinLength(int length, string message)
    {
        if (ShouldValidate())
        {
            if (Value is not string str || str.Length < length)
                _parent.AddNotification(_propertyName, message);
        }
        return this;
    }

    private static string GetPropertyName(Expression<Func<T, object>> expression)
    {
        if (expression.Body is MemberExpression member)
            return member.Member.Name;

        if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression operand)
            return operand.Member.Name;

        throw new InvalidOperationException("Não foi possível obter o nome da propriedade.");
    }

    private static object GetPropertyValue<T>(T instance, Expression<Func<T, object>> expression)
    {
        var func = expression.Compile();
        return func(instance);
    }
}
