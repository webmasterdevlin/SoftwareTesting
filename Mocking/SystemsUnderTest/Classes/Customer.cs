namespace Mocking.SystemsUnderTest.Classes;

public class Customer
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }

    public virtual Address AddressNavigation { get; set; }
}