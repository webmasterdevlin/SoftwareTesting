namespace Mocking.SystemsUnderTest.Classes;

public abstract class Address
{
    public virtual int Id { get; set; }
    public virtual int StreetNumber { get; set; }
    public virtual string StreetName { get; set; }
    public virtual string StateCode { get; set; }
    public virtual string Country { get; set; }
}