namespace DALIntegrationTests;

/// <summary>
/// The TestHelpers class is a utility class providing methods for setting up and configuring database contexts and related objects for testing.
/// It abstracts the details of configuration and context creation, making the test code cleaner and more maintainable.
/// It's a common pattern in integration testing to isolate database setup logic to ensure consistent testing environments.
/// </summary>
public static class TestHelpers
{
    /// <summary>
    /// This method sets up and returns an IConfiguration object that represents the application's configuration settings.
    /// </summary>
    /// <returns>IConfiguration</returns>
    public static IConfiguration GetConfiguration() =>
        new ConfigurationBuilder()
            // Setting the base path to the current directory, which is typically where the application is being run.
            .SetBasePath(Directory.GetCurrentDirectory())
            // Reading the appsettings.testing.json file. This JSON file is expected to contain configuration settings specifically for testing.
            .AddJsonFile("appsettings.testing.json", true, true)
            .Build();

    /// <summary>
    /// This method is useful for creating a database context with a connection to the database, specifically configured for testing.
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns>ApplicationDbContext</returns>
    public static ApplicationDbContext GetContext(IConfiguration configuration)
    {
        // It initializes a new DbContextOptionsBuilder<ApplicationDbContext> object, which is used to configure the options for the database context.
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        // It retrieves the connection string named "SoftwareTesting" from the provided configuration object.
        var connectionString = configuration.GetConnectionString("SoftwareTesting");
        // It sets the connection string to use SQL Server as the database provider.
        optionsBuilder.UseSqlServer(connectionString);
        // It constructs a new instance of ApplicationDbContext using the configured options and returns it.
        return new ApplicationDbContext(optionsBuilder.Options);
    }

    /// <summary>
    /// This method is useful when a second context is needed within the same transaction, such as in scenarios where multiple database operations must be coordinated within a single transaction.
    /// </summary>
    /// <param name="oldContext"></param>
    /// <param name="trans"></param>
    /// <returns></returns>
    public static ApplicationDbContext GetSecondContext(ApplicationDbContext oldContext,
        IDbContextTransaction trans)
    {
        // It initializes a new DbContextOptionsBuilder<ApplicationDbContext> object.
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        // It sets the database connection to use the same connection as the provided old context.
        optionsBuilder.UseSqlServer(oldContext.Database.GetDbConnection());
        // It creates a new ApplicationDbContext instance with the configured options.
        var context = new ApplicationDbContext(optionsBuilder.Options);
        // It sets the new context to use the provided database transaction.
        context.Database.UseTransaction(trans.GetDbTransaction());
        return context;
    }
}