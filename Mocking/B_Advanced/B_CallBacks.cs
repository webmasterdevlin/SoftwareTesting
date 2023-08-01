namespace Mocking.B_Advanced;

public class CallBacks
{
    private readonly ITestOutputHelper _output;

    public CallBacks(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Should_Call_Back_Before_And_After_Executing_Mocked_Function()
    {
        var mockRepo = new Mock<IRepo>();
        mockRepo.Setup(x => x.Find(It.IsAny<int>()))
            .Callback(() => _output.WriteLine("Before Execution"))
            .Returns(new Customer())
            .Callback(() => _output.WriteLine("After Execution"));
        var sut = new TestController(mockRepo.Object);
        sut.GetCustomer(12);
        mockRepo.VerifyAll();
    }
    
    [Fact]
    public void Should_Get_Argument_In_Call_Backs()
    {
        var mockRepo = new Mock<IRepo>();
        mockRepo.Setup(x => x.Find(It.IsAny<int>()))
            .Callback((int i) => _output.WriteLine($"Before Execution {i}"))
            .Returns(new Customer())
            .Callback((int i) => _output.WriteLine($"After Execution {i}"));
        var sut = new TestController(mockRepo.Object);
        sut.GetCustomer(12);
        mockRepo.VerifyAll();
    }
}