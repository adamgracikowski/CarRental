using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.DTOs.Segments;

namespace CarRental.Provider.API.Profiles;

public sealed class SegmentProfile : Profile
{
    public SegmentProfile()
    {
        CreateMap<Segment, SegmentDto>();
    }
}