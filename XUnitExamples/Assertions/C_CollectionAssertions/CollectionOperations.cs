namespace XUnitExamples.Assertions.C_CollectionAssertions;

public class CollectionOperations : BaseTestClass
{
    public CollectionOperations(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void SimpleCollectionAsserts()
    {
        List<int> list1 = new();

        Assert.Empty(list1);
        list1.Should().BeEmpty();
        list1.Add(0);
        Assert.NotEmpty(list1);
        Assert.Single(list1);
        list1.Should().NotBeEmpty();
        list1.Should().ContainSingle();

        Assert.Single(list1, 0);
        Assert.Single<int>(list1);
        list1.Add(1);
        Assert.Single<int>(list1, x => x < 1);
        Assert.All<int>(list1, x => Assert.True(x >= 0));
        list1.Should().ContainSingle(x => x < 1);
        list1.Should().OnlyContain(x => x >= 0);

        Assert.Contains<int>(1, list1);
        Assert.DoesNotContain<int>(2, list1);
        list1.Should().Contain(1);
        list1.Should().NotContain(2);
    }

    [Fact]
    public void DictionaryAsserts()
    {
        //Getting compile error if using var
        IDictionary<int, int> dictionary1 = new Dictionary<int, int>();
        Assert.DoesNotContain<int, int>(1, dictionary1);
        dictionary1.Should().NotContainKey(1);
        dictionary1.Add(0, 0);
        Assert.Contains<int, int>(0, dictionary1);
        dictionary1.Should().ContainKey(0);

        IReadOnlyDictionary<int, int> readOnly = new ReadOnlyDictionary<int, int>(dictionary1);

        Assert.Contains<int, int>(0, readOnly);
        Assert.DoesNotContain<int, int>(1, readOnly);
        readOnly.Should().ContainKey(0);
        readOnly.Should().NotContainKey(1);
    }

}