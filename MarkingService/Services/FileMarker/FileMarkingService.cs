using MarkingService.Entities;

namespace MarkingService.Services.FileMarker;

public class FileMarkingService : IFileMarkingService
{
    private readonly IFileMarkerFactory _fileMarkerFactory;
    private readonly ILogger<FileMarkingService> _logger;

    public FileMarkingService(IFileMarkerFactory fileMarkerFactory, ILogger<FileMarkingService> logger)
    {
        _fileMarkerFactory = fileMarkerFactory ?? throw new ArgumentNullException(nameof(fileMarkerFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public MarkedFile MarkFile(UnmarkedFile unmarkedFile)
    {
        // todo: ensure appropriate file marker is returned
        var fileMarker = _fileMarkerFactory.GetFileMarker(unmarkedFile.FileType);

        // todo: ensure file marking returns marked file
        var markedFile = fileMarker.Mark(unmarkedFile);

        return markedFile;
    }
}