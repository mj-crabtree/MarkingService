using MarkingService.Entities;

namespace MarkingService.Services.FileMarker;

public interface IFileMarker
{
    string HandlerFormat { get; }
    MarkedFile Mark(UnmarkedFile unmarkedFile);
}