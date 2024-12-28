using CSVTest.Business.Services.Intergaces;
using CSVTest.DataTransfer.Trips;
using Microsoft.AspNetCore.Mvc;

namespace CSVTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }
        
        [HttpGet("highest-tip-average")]
        public async Task<ActionResult<LocationTipAmountDto>> GetHighestTipAverage()
        {
            return await _tripService.GetLocationByMaxAvgTipAmount();

        }
        [HttpGet("top-trip")]
        public async Task<ActionResult<List<TripDto>>> GetToFaresOfTripDistance()
        {
            return await _tripService.GetFaresByTripDistance(100);

        }
        [HttpGet("top-time-spent")]
        public async Task<ActionResult<List<TripDto>>>  GetTopFaresOfTimeSpent()
        {
            return await _tripService.GetFaresByTripDistance(100);
        }
        [HttpGet("location/{puLocationId}")]
        public async Task<ActionResult<List<TripDto>>> GetByPuLocationId(int puLocationId)
        {
            return await _tripService.GetByPuLocationId(puLocationId);
        }
    }
}
