using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.Requests.Segments.DTOs;

namespace CarRental.Provider.API.Profiles;

public sealed class SegmentProfile : Profile
{
    public SegmentProfile()
    {
        CreateMap<Segment, SegmentDto>();
    }
}