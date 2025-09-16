namespace FluentRule.Tests.Models;
internal class CustomerBuilder
{
    private Guid _id = new();
    private string _fullName = "Fulano de Tal";
    private int _personType = 1;
    private string _document = "12233445567";
    private int _age = 30;
    private DateTime _creationDate = DateTime.Now.AddMonths(-5);

    public CustomerBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public CustomerBuilder WithFullName(string fullName)
    {
        _fullName = fullName;
        return this;
    }

    public CustomerBuilder WithPersonType(int personType)
    {
        _personType = personType;
        return this;
    }

    public CustomerBuilder WithDocument(string document)
    {
        _document = document;
        return this;
    }

    public CustomerBuilder WithAge(int age)
    {
        _age = age;
        return this;
    }

    public CustomerBuilder WithCreationDate(DateTime creationDate)
    {
        _creationDate = creationDate;
        return this;
    }

    public Customer Build()
    {
        return new(
            id: _id,
            fullName: _fullName,
            personType: _personType,
            document: _document,
            age: _age,
            creationDate: _creationDate);
    }
}