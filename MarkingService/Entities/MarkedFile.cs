namespace MarkingService.Entities;

public class MarkedFile
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
}