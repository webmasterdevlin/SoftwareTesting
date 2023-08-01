namespace Mocking.SystemsUnderTest;

public class TestController
{
    private readonly IRepo _repo;
    private readonly ILogger _logger;

    public TestController(IRepo repo, ILogger logger = null)
    {
        _repo = repo;
        _logger = logger;
        _repo.FailedDatabaseRequest += Repo_FailedDatabaseRequest;
    }

    private void Repo_FailedDatabaseRequest(object sender, EventArgs e)
    {
        _logger.Error("An error occurred");
    }

    public int TenantId() => _repo.TenantId;
    public void SetTenantId(int id) => _repo.TenantId = id;
    
    public Customer GetCurrentCustomer => _repo.CurrentCustomer;
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

    public async Task<int> GetCustomerCountAsync() => await _repo.GetCountAsync();

    public void SaveCustomer(Customer customer)
    {
        _repo.AddRecord(customer);
    }
}