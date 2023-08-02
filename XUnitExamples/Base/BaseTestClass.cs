namespace XUnitExamples.Base;

// abstract means that this class cannot be instantiated
public abstract class BaseTestClass : IDisposable
{
    protected readonly ITestOutputHelper TestOutputWriter;
    
    
    protected BaseTestClass(ITestOutputHelper output)
    {
        TestOutputWriter = output;
    }

    // dispose is called after each test
    public virtual void Dispose()
    {
    }
}