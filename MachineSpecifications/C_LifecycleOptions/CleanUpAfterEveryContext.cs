namespace MachineSpecifications.C_LifecycleOptions;

public class CleanUpAfterEveryContext : ICleanupAfterEveryContextInAssembly
{
    public void AfterContextCleanup()
    {
        //Runs after every context in assembly 
        //eliminates need for repeated code in Cleanup 
        var foobar = "foobar";
    }
}