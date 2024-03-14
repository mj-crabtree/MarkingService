namespace MarkingService.FileTypes;

public abstract class FileFormat
{
    private string TypeName;
    private string Extension;
    private byte[] LeadingBytes;
}

public class PortableDocumentFormat : FileFormat
{
    public const string TypeName = "Portable Document Format";
    public const string Extension = "pdf";
    private static readonly byte[] LeadingBytes = { 0x25, 0x50, 0x44, 0x46, 0x2D };
}

public class JointPhotographicExpertsGroup : FileFormat
{
    public const string TypeName = "Joint Photographic Experts Group";
    public const string Extension = "jpg";
    private static readonly byte[] LeadingBytes = { 0xFF, 0xD8, 0xFF };
}