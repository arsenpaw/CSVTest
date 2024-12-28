


using CSVTest.DataAccess.Contexts;
using CSVTest.DataAccess.Repositories.Interfaces;

namespace CSVTest.DataAccess.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly CsvContext Context;

        public BaseRepository(CsvContext context) => Context = context;

        public Task<int> SaveAsync() => Context.SaveChangesAsync();
    }

}
