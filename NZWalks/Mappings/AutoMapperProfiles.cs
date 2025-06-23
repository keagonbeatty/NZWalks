using AutoMapper;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;

namespace NZWalks.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<AddRegionRequestDto, Region>().ReverseMap();
        CreateMap<UpdateRegionDto, Region>().ReverseMap();
        CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
        CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        CreateMap<UpdateWalkRequest, Walk>().ReverseMap();
    }
}