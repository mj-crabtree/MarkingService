using MarkingService.Entities;

namespace MarkingService.Services;

public class FileSystemService : IFileSystemService
{
    public FileStream OpenFile(string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
    }
}