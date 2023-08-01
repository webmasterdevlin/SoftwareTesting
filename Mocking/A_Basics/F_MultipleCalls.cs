namespace Mocking.A_Basics;

public class MultipleCalls
{
    [Fact]
    public void Should_Mock_Repetitive_Function_Calls_With_Return_Values()
    {
        var id1 = 12;
        var name1 = "Fred FlintStone";
        var customer1 = new Customer { Id = id1, Name = name1 };
        var id2 = 1;
        var name2 = "Wilma FlintStone";
        var customer2 = new Customer { Id = id2, Name = name2 };
        var mock = new Mock<IRepo>();
        mock.SetupSequence(x => x.Find(It.IsAny<int>()))
            .Returns(customer1)
            .Returns(customer2);
        var controller = new TestController(mock.Object);
        var actual = controller.GetCustomer(id1);
        Assert.Same(customer1, actual);
        Assert.Equal(id1, actual.Id);
        Assert.Equal(name1, actual.Name);
        actual = controller.GetCustomer(id2);
        Assert.Same(customer2, actual);
        Assert.Equal(id2, actual.Id);
        Assert.Equal(name2, actual.Name);
        actual = controller.GetCustomer(id2);
        Assert.Null(actual);
        mock.Verify(x => x.Find(It.IsAny<int>()), Times.Exactly(3));
    }
}