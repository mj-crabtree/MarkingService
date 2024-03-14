namespace MarkingService.FileEventContexts;

public class FileSharedEventContext : IEventContext
{
    public Guid RecipientId { get; set; }
    public Guid ConversationId { get; set; }
}