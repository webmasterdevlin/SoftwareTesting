namespace XUnitExamples.Theories;

public class ClassDataTests : BaseTestClass
{
    public ClassDataTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }


    [Theory]
    [ClassData(typeof(TestDataClass))]
    public void ShouldReturnProperValue(RecordForTestData data)
    {
        TestOutputWriter.WriteLine($"{data.Input}:{data.ExpectedResult}");
    }
}