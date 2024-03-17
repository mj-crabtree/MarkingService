namespace MarkingService.Entities;

public class UnmarkedFile
{
    public string Path { get; set; }
    public byte[] Data { get; set; }
    public string FileType { get; set; } //application.pdf
    public ClassificationTier ClassificationTier { get; set; }
}