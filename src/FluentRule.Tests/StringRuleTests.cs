using FluentRule.RuleExtensions;
using FluentRule.Tests.Models;

namespace FluentRule.Tests;
public class StringRuleTests
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("", true)]
    [InlineData(" ", true)]
    [Trait("StringRule", "NotNull")]
    public void Validate_when_string_is_not_null(string value, bool expected)
    {
        Customer customer = new CustomerBuilder()
            .WithFullName(value)
            .Build();

        Contract<Customer> contract = new(customer);
        contract.RuleFor(p => p.FullName).NotNull("Some message");
        customer.AddNotifications(contract.Notifications);

        Assert.Equal(expected, customer.IsValid);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", true)]
    [Trait("StringRule", "NotNullOrEmpty")]
    public void Validate_when_string_is_not_null_or_empty(string value, bool expected)
    {
        Customer customer = new CustomerBuilder()
            .WithFullName(value)
            .Build();

        Contract<Customer> contract = new(customer);
        contract.RuleFor(p => p.FullName).NotNullOrEmpty("Some message");
        customer.AddNotifications(contract.Notifications);

        Assert.Equal(expected, customer.IsValid);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [Trait("StringRule", "NotNullOrWhitespace")]
    public void Validate_when_string_is_not_null_or_white_space(string value, bool expected)
    {
        Customer customer = new CustomerBuilder()
            .WithFullName(value)
            .Build();

        Contract<Customer> contract = new(customer);
        contract.RuleFor(p => p.FullName).NotNullOrWhiteSpace("Some message");
        customer.AddNotifications(contract.Notifications);

        Assert.Equal(expected, customer.IsValid);
    }
}