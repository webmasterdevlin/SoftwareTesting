namespace MachineSpecifications.B_Behaviors;

[Tags("Behaviors")]
[Subject("Authentication")]
public class When_using_behaviors
{
    static SampleService _subject;
    protected static SampleToken _token;

    //Arrange - only executes once for the class
    Establish _context = () => _subject = new SampleService();

    //Context Teardown
    private Cleanup _after = () => _subject.Dispose();

    //Act
    Because _of = () => _token = _subject.Authenticate("username", "password");
    //Asserts
    private Behaves_like<AuthenticationBehaviors> Checking_token_values;
}