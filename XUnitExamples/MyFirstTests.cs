namespace XUnitExamples;
public class MyFirstTests
{
    //demo the AAA syntax
    [Fact]
    public void ShouldReturnTheSum()
    {
        //arrange
        var addend1 = 2;
        var addend2 = 3;

        //act
        var sum = addend1 + addend2;

        // assert
        // xUnit assertions
        Assert.Equal(5, sum);
        // FluentAssertions
        sum.Should().Be(5);
    }
}
