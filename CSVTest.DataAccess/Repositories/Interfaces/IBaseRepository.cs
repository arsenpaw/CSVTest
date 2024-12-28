namespace CSVTest.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        Task<int> SaveAsync();
    }
}