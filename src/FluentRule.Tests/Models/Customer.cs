using FluentRule.RuleExtensions;

namespace FluentRule.Tests.Models;
public class Customer(
    Guid id,
    string fullName,
    int personType,
    string document,
    int age,
    DateTime creationDate) : Notifiable
{
    public Guid Id { get; private set; } = id;
    public string FullName { get; set; } = fullName;
    public int PersonType { get; set; } = personType;
    public string Document { get; set; } = document;
    public int Age { get; set; } = age;
    public DateTime CreationDate { get; set; } = creationDate;

    public static Customer Create(
        string fullName,
        int personType,
        string document,
        int age,
        DateTime originDate)
    {
        Customer customer = new(new Guid(), fullName, personType, document, age, originDate);

        // (1)
        //customer.AddNotification("");

        // (2)
        Contract<Customer> contract = new(customer);
        contract.RuleFor(p => p.FullName).NotNullOrEmpty().WithMessage("Fullname is required").HasMinLength(3);//.WithMessage("Fullname invalid min length");
        contract.RuleFor(p => p.Document).When(p => p.Document.StartsWith("1")).HasMinLength(15).WithMessage("Invalid Document");
        contract.RuleFor(p => p.PersonType).Satisfies(p => p > -1).WithMessage("Invalid PersonType");

        customer.AddNotifications(contract.Notifications);

        return customer;
    }
}
