namespace DALIntegrationTests.IntegrationTests;

[Collection("Integration Tests")]
public class CarTests : BaseTest, IClassFixture<EnsureAutoLoadDatabaseTestFixture>
{
    private readonly ICarRepo _carRepo;
    public CarTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _carRepo = new CarRepo(Context);
    }
    public override void Dispose()
    {
        _carRepo.Dispose();
        base.Dispose();
    }

    public static IEnumerable<object[]> CarCountByMakeId
        => new[]
        {
            new object[] { 1, 2 },
            new object[] { 2, 1 },
            new object[] { 3, 1},
            new object[] { 4, 2 },
            new object[] { 5, 3 },
            new object[] { 6, 1 }
        };

    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    [InlineData(4, 2)]
    [InlineData(5, 3)]
    [InlineData(6, 1)]
    public void ShouldGetTheCarsByMake(int makeId, int expectedCount)
    {
        // IgnoreQueryFilters is a query that includes the soft-deleted records
        IQueryable<Car> query = Context.Cars.IgnoreQueryFilters().Where(x => x.MakeId == makeId);
        var qs = query.ToQueryString();
        OutputHelper.WriteLine($"Query: {qs}");
        var carsCount = query.Count();
        Assert.Equal(expectedCount, carsCount);
        carsCount.Should().Be(expectedCount);
    }

    [Theory]
    [MemberData(nameof(CarCountByMakeId))]
    public void ShouldGetTheCarsByMakeUsingTheRepo(int makeId, int expectedCount)
    {
        var qs = _carRepo.GetAllBy(makeId).AsQueryable().ToQueryString();
        OutputHelper.WriteLine($"Query: {qs}");
        var cars = _carRepo.GetAllBy(makeId).ToList();
        Assert.Equal(expectedCount, cars.Count);
        cars.Count.Should().Be(expectedCount);
    }

    [Fact]
    public void ShouldGetAllOfTheCarsWithMakes()
    {
        IIncludableQueryable<Car, Make> query = Context.Cars.Include(c => c.MakeNavigation);
        var qs = query.ToQueryString();
        OutputHelper.WriteLine($"Query: {qs}");
        var carsCount = query.Count();
        Assert.Equal(10, carsCount);
        carsCount.Should().Be(10);
    }

    [Fact]
    public void ShouldAddACar()
    {
        ExecuteInATransaction(RunTheTest);

        void RunTheTest()
        {
            var car = new Car
            {
                Color = "Yellow",
                MakeId = 1,
                PetName = "Herbie"
            };
            var carCount = Context.Cars.Count();
            Context.Cars.Add(car);
            Context.SaveChanges();
            var newCarCount = Context.Cars.Count();
            Assert.Equal(carCount + 1, newCarCount);
            newCarCount.Should().Be(carCount + 1);
        }
    }

    [Fact]
    public void ShouldUpdateACar()
    {
        ExecuteInASharedTransaction(RunTheTest);

        void RunTheTest(IDbContextTransaction trans)
        {
            var car = Context.Cars.First(c => c.Id == 1);
            Assert.Equal("Black", car.Color);
            car.Color.Should().Be("Black");
            car.Color = "White";
            Context.SaveChanges();
            Context.ChangeTracker.Clear();
            Assert.Equal("White", car.Color);
            car.Color.Should().Be("White");
            var context2 = TestHelpers.GetSecondContext(Context, trans);
            var car2 = context2.Cars.First(c => c.Id == 1);
            Assert.Equal("White", car2.Color);
            car2.Color.Should().Be("White");
        }
    }

    [Fact]
    public void ShouldRemoveACar()
    {
        ExecuteInATransaction(RunTheTest);

        void RunTheTest()
        {
            var carCount = Context.Cars.Count();
            var car = Context.Cars.First(c => c.Id == 9);
            Context.Cars.Remove(car);
            Context.SaveChanges();
            var newCarCount = Context.Cars.Count();
            Assert.Equal(carCount - 1, newCarCount);
            Assert.Equal(EntityState.Detached, Context.Entry(car).State);
            newCarCount.Should().Be(carCount - 1);
            Context.Entry(car).State.Should().Be(EntityState.Detached);
        }
    }
    [Fact]
    public void ShouldThrowConcurrencyException()
    {
        ExecuteInATransaction(RunTheTest);

        void RunTheTest()
        {
            //Get a car record (does not matter which one)
            var car = Context.Cars.First();
            //Update the database outside of the context
            Context.Database.ExecuteSqlInterpolated($"Update dbo.Inventory set Color='Pink' where Id = {car.Id}");
            //update the car record in the change tracker
            car.Color = "Yellow";
            var ex = Assert.Throws<CustomConcurrencyException>(() => Context.SaveChanges());
            Action act = () => Context.SaveChanges();
            act.Should().Throw<CustomConcurrencyException>().And.InnerException.Should().BeOfType<DbUpdateConcurrencyException>();
        }
    }
}
