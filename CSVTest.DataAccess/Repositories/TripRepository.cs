using CSVTest.DataAccess.Contexts;
using CSVTest.DataAccess.Entities;
using CSVTest.DataAccess.Repositories.Interfaces;

namespace CSVTest.DataAccess.Repositories;

public class TripRepository: BaseRepository, ITripRepository
{
    public TripRepository(CsvContext context) : base(context)
    {
    }

    public IQueryable<Trip> GetAll() => Context.Trips;
    
    public IQueryable<Trip> GetByIdAsync(Guid id) => Context.Trips.Where(x => x.Id == id);
    
    
}