using MarkingService.Contexts;
using MarkingService.Options;
using MarkingService.Services;
using MarkingService.Services.Builders;
using MarkingService.Services.FileMarker;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/markingService.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

#region CustomMiddleware

builder.Host.UseSerilog();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<FilesDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<IFileSystemService, FileSystemService>();
builder.Services.AddScoped<IFileMarkerFactory, FileMarkerFactory>();
builder.Services.AddScoped<IFileMarkingService, FileMarkingService>();
builder.Services.AddScoped<IFileMarker, PdfFileMarker>();
builder.Services.AddScoped<UnmarkedFileBuilder>();

builder.Services.Configure<FilePathOptions>(
    builder.Configuration.GetSection(FilePathOptions.Paths));

builder.Services.AddHttpClient();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();