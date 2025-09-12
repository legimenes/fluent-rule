using FluentNotification;

namespace FluentRule.Tests.Models;
public class Customer(
    Guid id,
    string fullName,
    int personType,
    string document,
    DateTime originDate) : Notifiable
{
    public Guid Id { get; private set; } = id;
    public string FullName { get; set; } = fullName;
    public int PersonType { get; set; } = personType;
    public string Document { get; set; } = document;
    public DateTime OriginDate { get; set; } = originDate;

    public static Customer Create(
        string fullName,
        int personType,
        string document,
        DateTime originDate)
    {
        Customer customer = new(new Guid(), fullName, personType, document, originDate);

        // (1)
        //customer.AddNotification("");

        // (2)
        Contract<Customer> contract = new(customer);
        contract.RuleFor(p => p.FullName).IsNotNullOrEmpty("Fullname is required");
        contract.RuleFor(p => p.Document).When(p => p.Document.StartsWith("1")).HasMinLength(15, "Invalid Document");

        customer.AddNotifications(contract.Notifications);

        return customer;
    }
}
