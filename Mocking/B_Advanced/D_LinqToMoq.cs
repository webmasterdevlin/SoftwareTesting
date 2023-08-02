namespace Mocking.B_Advanced;

public class LinqToMoq
{
    [Fact]
    public void Should_Use_Linq()
    {
        //Arrange
        var id = 12;
        var name = "Fred Flintstone";
        var customer = new Customer { Id = id, Name = name };
        var mockRepo =
            Mock.Of<IRepo>(r => r.Find(id) == customer && r.TenantId == 5 && r.CurrentCustomer == customer,
                MockBehavior.Strict);

        var controller = new TestController(mockRepo);
        
        //Act
        var custFromController2 = controller.GetCurrentCustomer;
        
        //Assert
        Assert.Same(customer, custFromController2);
        Assert.Equal(id, custFromController2.Id);
        Assert.Equal(name, custFromController2.Name);
        custFromController2.Should().BeSameAs(customer);
        custFromController2.Id.Should().Be(id);
        custFromController2.Name.Should().Be(name);
    }
}