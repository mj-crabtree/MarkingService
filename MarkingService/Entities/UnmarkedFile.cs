namespace MarkingService.Entities;

public class UnmarkedFile
{
    public string FileLocation { get; set; }
    public IFormFile File { get; set; }
    public string FileType { get; set; }
}