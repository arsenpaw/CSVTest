using AutoMapper;
using CSVTest.Business.Services.Intergaces;
using CSVTest.DataAccess.Repositories.Interfaces;
using CSVTest.DataTransfer.Trips;
using CSVTest.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CSVTest.Business.Services;

public class TripService : ITripService
{
    public readonly ITripRepository _tripRepository;
    private readonly IMapper _mapper;
    
    
    public TripService(ITripRepository tripRepository, IMapper mapper)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
    }
    public async Task<List<TripDto>> GetByPuLocationId(int puLocationId)
    {
        var trips = await _tripRepository.GetAll()
            .Where(x => x.PuLocationId == puLocationId)
            .ToListAsync();
        if (!trips.Any())
        {
            throw new AppException(ExceptionMessages.NotFound);
        }
        return _mapper.Map<List<TripDto>>(trips);
    }
    public async Task<LocationTipAmountDto> GetLocationByMaxAvgTipAmount()
    {
        var res = await _tripRepository.GetAll()
            .GroupBy(t => t.PuLocationId)
            .Select(g => new LocationTipAmountDto()
            {
                PuLocationId = g.Key,
                AvgTipAmount = g.Average(t => t.TipAmount)
            })
            .OrderByDescending(x => x.AvgTipAmount)
            .FirstOrDefaultAsync();
        if (res == null)
        {
           throw new AppException(ExceptionMessages.NotFound);
        }
        return res;
    }
    public async Task<List<TripDto>> GetFaresByTripDistance(int limiter)
    {
        var trips = await _tripRepository.GetAll()
            .OrderByDescending(x => x.TripDistance)
            .Take(limiter)
            .ToListAsync();
        if (!trips.Any())
        {
            throw new AppException(ExceptionMessages.NotFound);
        }
        return _mapper.Map<List<TripDto>>(trips);
    }
    
    public async Task<List<TripDto>> GetTopFaresOfTimeSpent(int limiter)
    {
        var trips = await _tripRepository.GetAll()
            .OrderByDescending(x => EF.Functions.DateDiffSecond(x.PickupDatetime, x.DropoffDatetime))
            .Take(limiter)
            .ToListAsync();
        if (!trips.Any())
        {
            throw new AppException(ExceptionMessages.NotFound);
        }
        return _mapper.Map<List<TripDto>>(trips);
    }
    

}

