using CSVTest.DataTransfer.Trips;

namespace CSVTest.Business.Services.Intergaces;

public interface ITripService
{
    public Task<LocationTipAmountDto> GetLocationByMaxAvgTipAmount();

    Task<List<TripDto>> GetFaresByTripDistance(int limiter);

    Task<List<TripDto>> GetTopFaresOfTimeSpent(int limiter);
    Task<List<TripDto>> GetByPuLocationId(int puLocationId);
}
