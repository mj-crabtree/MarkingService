namespace MarkingService.FileEventContexts;

public class FileCreationEventContext : IEventContext
{
    public string FileName { get; set; }
    public string FileLength { get; set; }
    public string FileType { get; set; }
    public string Uri { get; set; }
}