using AutoMapper;
using MarkingService.Controllers;
using MarkingService.Entities;
using MarkingService.Services;

namespace MarkingService.Profiles;

public class FileMapper : Profile
{
    public FileMapper()
    {
        CreateMap<UnmarkedFileDto, UnmarkedFile>();
        CreateMap<MarkedFile, MarkedFileDto>();
    }
}