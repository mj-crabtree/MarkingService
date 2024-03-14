using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace MarkingService.Services;

class AuditServiceClient : IAuditServiceClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string AuditUrl = "http://localhost:5100/api/events";

    public AuditServiceClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task CreateAuditEvent()
    {
        var client = _httpClientFactory.CreateClient();
        var jsonBody = BuildJsonPayload();
        using var response = await client.PostAsync(AuditUrl, jsonBody);
        response.EnsureSuccessStatusCode();
    }

    private StringContent BuildJsonPayload()
    {
        return new StringContent(
            JsonSerializer.Serialize(
                new
                {
                    OperationTimeStamp = DateTime.Now,
                    TrackedFileId = "123e4567-e89b-12d3-a456-426614174000",
                    TrackedUserId = "234e5678-e89b-12d3-a456-426614174001",
                    EventType = "FileClassified",
                    OldClassificationTier = "Official",
                    NewClassificationTier = "Secret",
                    SuccessfulClassification = true,
                    CurrentClassification = "Secret"
                }),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
    }
}