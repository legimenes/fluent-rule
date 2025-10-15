using FluentRule.RuleExtensions;

namespace FluentRule.Tests.Models;

public class Customer(
    Guid id,
    string fullName,
    int personType,
    string personTypeDescription,
    string document,
    int age,
    DateTime creationDate) : Notifiable
{
    public Guid Id { get; private set; } = id;
    public string FullName { get; set; } = fullName;
    public int PersonType { get; set; } = personType;
    public string PersonTypeDescription { get; set; } = personTypeDescription;
    public string Document { get; set; } = document;
    public int Age { get; set; } = age;
    public DateTime CreationDate { get; set; } = creationDate;

    public static Customer Create(
        string fullName,
        int personType,
        string personTypeDescription,
        string document,
        int age,
        DateTime originDate)
    {
        Customer customer = new(new Guid(), fullName, personType, personTypeDescription, document, age, originDate);

        // (1)
        customer.AddNotification("Testando msg padrao");

        //MessageManager.CurrentLanguage = "pt-BR";

        // (2)
        Contract<Customer> contract = new(customer);
        contract.RuleFor(p => p.PersonType).Custom((pp, ctx) =>
        {
            var customer = ctx.Instance;
            if (customer.PersonType == -1)
                ctx.AddNotification("Erro 1");
        });
        contract.RuleFor(p => p.PersonType).Must(p =>
        {
            bool isInvalid = p == -1;
            if (isInvalid)
                return false;
            return true;
        });
        contract.RuleFor(p => p.PersonTypeDescription).IsEnumName(typeof(PersonType));
        contract.When(x => x.Age > 20 && x.Age < 30, () =>
        {
            contract.RuleFor(p => p.FullName).NotNullOrEmpty();
        });
        contract.RuleFor(p => p.FullName).NotNullOrEmpty();
        contract.RuleFor(p => p.FullName).NotNullOrEmpty().WithMessage("Fullname is required. Actual is {Value}").MinimumLength(3);//.WithMessage("Fullname invalid min length");
        contract.RuleFor(p => p.Document).When(p => p.Document.StartsWith('1')).MinimumLength(15).WithMessage("Invalid Document");
        contract.RuleFor(p => p.PersonType).Must(p => p > -1).WithMessage("Invalid PersonType");

        customer.AddNotifications(contract.Notifications);

        return customer;
    }
}
