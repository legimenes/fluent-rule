using FluentRule.Tests.Models;

namespace FluentRule.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Customer customer = Customer.Create("", -1, "12345678900", 20, DateTime.Now.AddYears(-5));
        bool isValid = customer.IsValid;
    }
}