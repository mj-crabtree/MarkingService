namespace MarkingService.Services;

public interface IFileSystemService
{
    FileStream OpenFile(string path);
}