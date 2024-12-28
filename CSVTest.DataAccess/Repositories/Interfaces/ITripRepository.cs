using CSVTest.DataAccess.Entities;

namespace CSVTest.DataAccess.Repositories.Interfaces;

public interface ITripRepository
{
    public IQueryable<Trip> GetAll();
    
    public IQueryable<Trip> GetByIdAsync(Guid id);
}