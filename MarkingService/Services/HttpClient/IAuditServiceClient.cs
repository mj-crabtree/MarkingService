namespace MarkingService.Services;

public interface IAuditServiceClient
{
    Task CreateAuditEvent();
}