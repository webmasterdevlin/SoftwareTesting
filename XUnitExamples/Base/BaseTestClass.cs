namespace XUnitExamples.Base;

public abstract class BaseTestClass : IDisposable
{
    protected readonly ITestOutputHelper TestOutputWriter;

    protected BaseTestClass(ITestOutputHelper output)
    {
        TestOutputWriter = output;
    }

    public virtual void Dispose()
    {
    }
}