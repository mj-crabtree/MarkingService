using MarkingService.Entities;

namespace MarkingService.Services;

public class FileMarkingService : IFileMarkingService
{
    private readonly IFileMarkerFactory _fileMarkerFactory;
    private readonly IFileSystemService _fileSystemService;
    private readonly ILogger<FileMarkingService> _logger;

    public FileMarkingService(IFileMarkerFactory fileMarkerFactory, ILogger<FileMarkingService> logger, IFileSystemService fileSystemService)
    {
        _fileMarkerFactory = fileMarkerFactory ?? throw new ArgumentNullException(nameof(fileMarkerFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
    }
    
    public MarkedFile MarkFile(UnmarkedFile unmarkedFile)
    {
        var fileMarker = _fileMarkerFactory.GetFileMarker(unmarkedFile.FileType);
        var fileToMark = _fileSystemService.OpenFile(unmarkedFile.FileLocation);
        var fileToReturn = fileMarker.ApplyMarking(fileToMark);
        return fileToReturn;
    }
}

public interface IFileSystemService
{
    FileStream OpenFile(string path);
}

public class FileSystemService : IFileSystemService
{
    public FileStream OpenFile(string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
    }
}

public interface IFileMarkerFactory
{
    IFileMarker GetFileMarker(string fileType);
}

public class FileMarkerFactory : IFileMarkerFactory
{
    // gives us the marker we need to process a given file
    public IFileMarker GetFileMarker(string fileType)
    {
        switch (fileType.ToLower())
        {
            case "application/pdf":
                return new PdfFileMarker();
            default:
                throw new ArgumentException(nameof(fileType));
        }
    }
}

public class PdfFileMarker : IFileMarker
{
    public MarkedFile ApplyMarking(FileStream unmarkedFileStream)
    {
        return new MarkedFile();
    }
    
    private PdfFileMarker ApplyVisualMarking()
    {
        throw new NotImplementedException();
    }

    private PdfFileMarker ApplyMetadataMarking()
    {
        throw new NotImplementedException();
    }
}

public interface IFileMarker
{
    MarkedFile ApplyMarking(FileStream unmarkedFileStream);
}