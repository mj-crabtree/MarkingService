using System.ComponentModel;
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
        var success = Enum.TryParse<ClassificationTier>(fileDto.ClassificationTier, out ClassificationTier result);
        if (!success)
        {
            throw new InvalidEnumArgumentException(nameof(fileDto.ClassificationTier));
        }

        _unmarkedFile.ClassificationTier = result;
        _unmarkedFile.FileType = Path.GetExtension(_unmarkedFile.Path);
        _unmarkedFile.Data = ReadFile(_unmarkedFile.Path);
        return this;
    }

    public UnmarkedFile Build()
    {
        return _unmarkedFile;
    }

    private byte[] ReadFile(string path)
    {
        try
        {
            return File.ReadAllBytes(path);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}