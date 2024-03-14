using MarkingService.Entities;

namespace MarkingService.Models.Dto;

public class UnmarkedFileDto
{
    // note: nothing comes in if it hasn't first been vetted by the upload service
    public string Path { get; set; }
    public ClassificationTier ClassificationTier { get; set; }
}