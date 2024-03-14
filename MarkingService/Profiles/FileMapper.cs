using AutoMapper;
using MarkingService.Controllers;
using MarkingService.Entities;
using MarkingService.Models.Dto;

namespace MarkingService.Profiles;

public class FileMapper : Profile
{
    public FileMapper()
    {
        CreateMap<UnmarkedFileDto, UnmarkedFile>();
        CreateMap<MarkedFile, MarkedFileDto>();
    }
}