namespace Mocking.A_Basics;

public class Exceptions
{
    [Fact]
    public void Should_Mock_General_Exceptions()
    {
        var id = 12;
        var mock = new Mock<IRepo>();
        mock.Setup(x => x.Find(id)).Throws<ArgumentException>();
        var controller = new TestController(mock.Object);
        mock.SetupGet(x => x.TenantId).Throws<ArgumentException>();
        Assert.Throws<ArgumentException>(() => controller.TenantId());
        mock.SetupSet(x => x.TenantId = id).Throws<ArgumentException>();
        Assert.Throws<ArgumentException>(() => controller.SetTenantId(12));
    }
}