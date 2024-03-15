using MarkingService.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarkingService.Contexts;

public class FilesDbContext : DbContext
{
    public DbSet<MarkedFile> Files { get; set; }
}