using AutoMapper;
using CSVTest.DataAccess.Entities;
using CSVTest.DataTransfer.Trips;

namespace CSVTest.Heplers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Trip, TripDto>();
        CreateMap<TripDto, Trip>();
    }
}