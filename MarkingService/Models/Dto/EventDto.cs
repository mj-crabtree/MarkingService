using MarkingService.FileEventContexts;

namespace MarkingService.Models.Dto;

public class EventDto
{
    // generic event props
    public Guid Id { get; set; }
    public Guid FileId { get; set; }
    public Guid UserId { get; set; } 
    public DateTime DateTime { get; set; }
    
    // specific event props
    public EventType EventType { get; set; }
    public IEventContext EventContext { get; set; }
}