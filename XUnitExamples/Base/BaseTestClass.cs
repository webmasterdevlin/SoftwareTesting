namespace XUnitExamples.Base
{
    /// <summary>
    /// An abstract base class for test classes, providing common functionality for managing test outputs.
    /// </summary>
    /// <remarks>
    /// The class is abstract, meaning it cannot be instantiated directly, and must be inherited by other classes.
    /// It also implements the IDisposable interface to handle any necessary cleanup after the test is run.
    /// </remarks>
    public abstract class BaseTestClass : IDisposable
    {
        /// <summary>
        /// An output helper that allows writing log messages to the test output.
        /// </summary>
        /// <remarks>
        /// This field is used to write diagnostic messages during testing.
        /// It can be used by inheriting classes to log additional information that may be useful for debugging.
        /// </remarks>
        protected readonly ITestOutputHelper TestOutputWriter;
        
        /// <summary>
        /// Initializes a new instance of the BaseTestClass with the specified output helper.
        /// </summary>
        /// <param name="output">An instance of ITestOutputHelper used for writing test outputs.</param>
        protected BaseTestClass(ITestOutputHelper output)
        {
            TestOutputWriter = output;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>
        /// This method is called after each test is run and can be overridden to perform custom cleanup logic.
        /// In the current implementation, it does nothing.
        /// </remarks>
        public virtual void Dispose()
        {
        }
    }
}