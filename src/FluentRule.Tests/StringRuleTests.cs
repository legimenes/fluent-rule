using FluentRule.RuleExtensions;
using FluentRule.Tests.Models;

namespace FluentRule.Tests;
public class StringRuleTests
{
    [Fact]
    [Trait("StringRule", "NotNull")]
    public void Validate_if_string_is_not_null()
    {
        Customer customer = new CustomerBuilder()
            .WithFullName(null!)
            .Build();

        Contract<Customer> contract = new(customer);
        contract.RuleFor(p => p.FullName).NotNull("Fullname is required");
        customer.AddNotifications(contract.Notifications);

        Assert.False(customer.IsValid);
    }
}