namespace MarkingService.Entities;

public enum ClassificationTier
{
    Secret,
    Official,
    TopSecret
}

public static class ClassificationTierExtensions
{
    public static string GetCopyText(this ClassificationTier tier)
    {
        switch (tier)
        {
            case ClassificationTier.Official:
                return "This is an Official document.";
            case ClassificationTier.Secret:
                return "This is a Secret document.";
            case ClassificationTier.TopSecret:
                return "This is a Top Secret document.";
            default:
                throw new ArgumentOutOfRangeException(nameof(tier), tier, "Invalid classification tier.");
        }
    }
}