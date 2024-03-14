using MarkingService.Entities;
using MarkingService.Models.Dto;

namespace MarkingService.Services.Builders;

public class UnmarkedFileBuilder
{
    private UnmarkedFile _unmarkedFile;

    public UnmarkedFileBuilder()
    {
        _unmarkedFile = new UnmarkedFile();
    }

    public UnmarkedFileBuilder WithDtoData(UnmarkedFileDto fileDto)
    {
        _unmarkedFile.Path = fileDto.Path;
        _unmarkedFile.ClassificationTier = fileDto.ClassificationTier;
        return this;
    }

    public UnmarkedFile Build()
    {
        return _unmarkedFile;
    }
}