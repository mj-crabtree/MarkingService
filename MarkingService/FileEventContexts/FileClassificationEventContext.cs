namespace MarkingService.FileEventContexts;

public class FileClassificationEventContext : IEventContext
{
    public string OriginalClassification { get; set; }
    public string TargetClassiciation { get; set; }
    public string CurrentClassification { get; set; }
    public bool SuccessfulClassification { get; set; }
}