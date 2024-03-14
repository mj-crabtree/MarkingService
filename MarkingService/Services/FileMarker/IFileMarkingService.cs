using MarkingService.Entities;

namespace MarkingService.Services;

public interface IFileMarkingService
{
    public MarkedFile MarkFile(UnmarkedFile unmarkedFile);
}