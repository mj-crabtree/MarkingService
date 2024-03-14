namespace MarkingService.Options;

public class FilePathOptions
{
    public const string Paths = "FilePathOptions";
    public string? Processed { get; set; }
    public string? Unprocessed { get; set; }
}