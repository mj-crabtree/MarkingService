namespace MarkingService.Services.FileMarker;

public interface IFileMarkerFactory
{
    IFileMarker GetFileMarker(string fileType);
}