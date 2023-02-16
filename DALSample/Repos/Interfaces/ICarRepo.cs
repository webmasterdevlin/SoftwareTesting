namespace DALSample.Repos.Interfaces;

public interface ICarRepo : IBaseRepo<Car>
{
    IEnumerable<Car> GetAllBy(int makeId);
}
