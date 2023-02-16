namespace MachineSpecifications.SystemUnderTest;

public class SampleService : IDisposable
{
    public SampleToken Authenticate(string userName, string password)
    {
        if (String.IsNullOrEmpty(userName))
        {
            throw new ArgumentNullException(nameof(userName), "Missing userName");
        }

        return new SampleToken { UserName = userName, Password = password };
    }

    public void Dispose()
    {

    }
}