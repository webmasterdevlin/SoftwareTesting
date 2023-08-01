namespace Mocking.A_Basics;

public class Arguments
{
    [Fact]
    public void Should_Return_Null_When_No_Argument_Match()
    {
        var id = 12;
        var name = "Fred Flintstone";
        var customer = new Customer { Id = id, Name = name };
        var mock = new Mock<IRepo>();
        mock.Setup(x => x.Find(id)).Returns(customer);
        var controller = new TestController(mock.Object);
        var actual = controller.GetCustomer(id);
        Assert.Null(actual);
    }

    [Fact]
    public void Should_Return_When_Arguments_Match()
    {
        var id = 11;
        var name = "Fred Flintstone";
        var customer = new Customer { Id = id, Name = name };
        var mock = new Mock<IRepo>();
        mock.Setup(x => x.Find(It.IsAny<int>())).Returns(customer);
        var controller = new TestController(mock.Object);
        var actual = controller.GetCustomer(id);
        Assert.Same(customer, actual);
        Assert.Equal(id, actual.Id);
        Assert.Equal(name, actual.Name);
    }
    
    [Fact]
    public void Should_Allow_Wildcards_On_Setters()
    {
        var mock = new Mock<IRepo>();
        var tenantId = 5;
        mock.SetupSet(SetAction);
        var controller = new TestController(mock.Object);
        controller.SetTenantId(tenantId);
        mock.VerifySet(SetAction);

        void SetAction(IRepo x) => x.TenantId = It.IsAny<int>();
    }

    [Fact]
    public void Should_Execute_With_Complex_Argument()
    {
        var id = 12;
        var name = "Fred Flintstone";
        var customer = new Customer { Id = id, Name = name };
        var mock = new Mock<IRepo>();
        mock.Setup(x =>
            x.AddRecord(It.Is<Customer>(x => x.Id == 12 &&
                  x.Name.StartsWith("Fred", StringComparison.OrdinalIgnoreCase)))).Verifiable();

        var controller = new TestController(mock.Object);
        controller.SaveCustomer(customer);
        mock.VerifyAll();
    }
}