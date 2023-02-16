namespace MachineSpecifications.C_LifecycleOptions;

public class SetupTeardownAllContexts : IAssemblyContext
{
    public void OnAssemblyStart()
    {
        //Runs before all specs
        var foo = "foo";
    }

    public void OnAssemblyComplete()
    {
        //runs after all specs;
        var bar = "bar";
    }
}