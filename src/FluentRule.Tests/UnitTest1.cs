using FluentRule.Tests.Extensions;
using FluentRule.Tests.Models;

namespace FluentRule.Tests;

public class UnitTest1
{
    public UnitTest1()
    {
        FluentRuleOptions.GlobalOptions.SetLanguage("pt-BR");
        FluentRuleOptions.GlobalOptions.UseProvider(new JsonMessageProvider("./Messages"));
    }

    [Fact]
    public void Test1()
    {
        Customer customer = Customer.Create("", -1, "PessoaFisica", "12345678900", 20, DateTime.Now.AddYears(-5));
        bool isValid = customer.IsValid;
    }
}