namespace Mocking.SystemsUnderTest.Interfaces;

public interface ILogger
{
    public void Error(string message);
    public void Debug(string message);
}