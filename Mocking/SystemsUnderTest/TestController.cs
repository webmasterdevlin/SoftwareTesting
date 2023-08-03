namespace Mocking.SystemsUnderTest;

/// <summary>
/// The class is designed to be part of a mocking test system, with dependencies injected that allow for flexible testing.
/// The handling of events and exceptions within the class provides opportunities to test various scenarios, such as failures in the repository layer.
/// </summary>
public class TestController
{
    private readonly IRepo _repo;
    private readonly ILogger _logger;

    /// <summary>
    /// The constructor subscribes to an event FailedDatabaseRequest on the repository.
    /// If this event is triggered, the Repo_FailedDatabaseRequest method will be called.
    /// </summary>
    /// <param name="repo">Required parameter, represents a repository interface to interact with some data.</param>
    /// <param name="logger">Optional parameter, represents a logging interface to log information and errors.</param>
    public TestController(IRepo repo, ILogger logger = null)
    {
        _repo = repo;
        _logger = logger;
        _repo.FailedDatabaseRequest += Repo_FailedDatabaseRequest; 
    }

    /// <summary>
    /// An event handler method that logs an error when the FailedDatabaseRequest event is raised.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Repo_FailedDatabaseRequest(object sender, EventArgs e)
    {
        _logger.Error("An error occurred");
    }

    /// <summary>
    /// A method to get the tenant ID from the repository.
    /// </summary>
    /// <returns></returns>
    public int TenantId() => _repo.TenantId;
    
    /// <summary>
    /// A method to set the tenant ID in the repository.
    /// </summary>
    /// <param name="id"></param>
    public void SetTenantId(int id) => _repo.TenantId = id;
    
    /// <summary>
    /// A property to get the current customer from the repository.
    /// </summary>
    public Customer GetCurrentCustomer => _repo.CurrentCustomer;
    
    /// <summary>
    /// A method that increments the given ID and then attempts to find a customer with that ID in the repository.
    /// If an exception occurs during this process, it logs a debug message and rethrows the exception.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Customer GetCustomer(int id)
    {
        try
        {
            id++;
            return _repo.Find(id);
        }
        catch (Exception ex)
        {
            _logger?.Debug("There was an exception");
            throw;
        }
    }

    /// <summary>
    /// An asynchronous method that gets the customer count from the repository.
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetCustomerCountAsync() => await _repo.GetCountAsync();

    /// <summary>
    /// A method that adds a customer record to the repository.
    /// </summary>
    /// <param name="customer"></param>
    public void SaveCustomer(Customer customer)
    {
        _repo.AddRecord(customer);
    }
}