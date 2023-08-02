namespace XUnitExamples.Theories;

public class ClassDataTests : BaseTestClass
{
    public ClassDataTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }


    [Theory] // the definition of theory is that it is a test that is executed multiple times with different data
    [ClassData(typeof(TestDataClass))]
    public void ShouldReturnProperValue(RecordForTestData data)
    {
        TestOutputWriter.WriteLine($"{data.Input}:{data.ExpectedResult}");
    }
}