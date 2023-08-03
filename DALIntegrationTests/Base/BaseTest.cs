namespace DALIntegrationTests.Base;

/// <summary>
/// The BaseTest class encapsulates common setup, teardown, and utility methods for database-related integration tests.
/// By inheriting from this class, individual test classes can leverage shared logic and structure, ensuring a consistent and maintainable testing approach.
/// </summary>
public abstract class BaseTest : IDisposable
{
    // Holds the application's configuration settings, typically loaded from a file like appsettings.testing.json.
    protected readonly IConfiguration Configuration;
    // Represents the database context used in the tests. It is an instance of ApplicationDbContext.
    protected readonly ApplicationDbContext Context;
    // An instance of ITestOutputHelper, used to write log messages or other diagnostic information related to the tests.
    protected readonly ITestOutputHelper OutputHelper;
    
    protected BaseTest(ITestOutputHelper outputHelper)
    {
        Configuration = TestHelpers.GetConfiguration();
        Context = TestHelpers.GetContext(Configuration);
        OutputHelper = outputHelper;
    }

    /// <summary>
    /// This method is used to release the resources held by the Context when the test is finished.
    /// It's part of the IDisposable pattern and is automatically called by the test runner when the test class is no longer needed.
    /// </summary>
    public virtual void Dispose()
    {
        Context.Dispose();
    }

    /// <summary>
    /// This allows the test to make changes to the database in isolation, without affecting other tests or the underlying data.
    /// </summary>
    /// <param name="actionToExecute">Executes within a database transaction. The transaction is rolled back after execution.</param>
    protected void ExecuteInATransaction(Action actionToExecute)
    {
        var strategy = Context.Database.CreateExecutionStrategy();
        strategy.Execute(() =>
        {
            using (var trans = Context.Database.BeginTransaction())
            {
                actionToExecute();
                trans.Rollback();
            }
        });
    }
    
    /// <summary>
    /// This method might be used in scenarios where you want to simulate shared transactions across different parts of the code under test.
    /// </summary>
    /// <param name="actionToExecute">Executes within a shared database transaction with the isolation level set to ReadUncommitted</param>
    protected void ExecuteInASharedTransaction(Action<IDbContextTransaction> actionToExecute)
    {
        var strategy = Context.Database.CreateExecutionStrategy();
        strategy.Execute(() =>
        {
            using IDbContextTransaction trans = Context.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
            actionToExecute(trans);
            trans.Rollback();
        });
    }
}
