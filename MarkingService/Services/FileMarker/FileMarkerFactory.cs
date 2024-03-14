using MarkingService.Services.FileMarker;

namespace MarkingService.Services;

public class FileMarkerFactory : IFileMarkerFactory
{
    private readonly IEnumerable<IFileMarker> _fileMarkers;

    public FileMarkerFactory(IEnumerable<IFileMarker> fileMarkers)
    {
        _fileMarkers = fileMarkers ?? throw new ArgumentNullException(nameof(fileMarkers));
    }
    
    public IFileMarker GetFileMarker(string fileFormat)
    {
        return _fileMarkers.FirstOrDefault(f => f.HandlerFormat == fileFormat) ?? throw new NotSupportedException(nameof(fileFormat));
    }
}