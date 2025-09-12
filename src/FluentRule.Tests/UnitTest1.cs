using FluentRule.Tests.Models;

namespace FluentRule.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Customer customer = Customer.Create("", 0, "12345678900", DateTime.Now.AddYears(-5));
        bool isValid = customer.IsValid;
    }
}