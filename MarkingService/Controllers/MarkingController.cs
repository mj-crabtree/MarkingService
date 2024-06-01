using AutoMapper;
using MarkingService.Contexts;
using MarkingService.Models.Dto;
using MarkingService.Services;
using MarkingService.Services.Builders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkingService.Controllers;

[ApiController]
[Route("/api/mark")]
public class MarkingController : ControllerBase
{
    private readonly FilesDbContext _context;
    private readonly IFileMarkingService _fileMarkingService;
    private readonly ILogger<MarkingController> _logger;
    private readonly IMapper _mapper;

    public MarkingController(IFileMarkingService fileMarkingService, IMapper mapper, ILogger<MarkingController> logger,
        FilesDbContext context)
    {
        _fileMarkingService = fileMarkingService ?? throw new ArgumentNullException(nameof(fileMarkingService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpPost]
    public async Task<ActionResult<MarkedFileDto>> MarkFile(UnmarkedFileDto unmarkedFileDto,
        UnmarkedFileBuilder unmarkedFileBuilder)
    {
        if (unmarkedFileDto == null)
        {
            return BadRequest();
        }

        if (unmarkedFileBuilder == null)
        {
            throw new ArgumentNullException(nameof(unmarkedFileBuilder));
        }

        var unmarkedFile = unmarkedFileBuilder
            .WithDtoData(unmarkedFileDto)
            .Build();

        var markedFile = _fileMarkingService.MarkFile(unmarkedFile);

        _context.Files.Add(markedFile);
        await _context.SaveChangesAsync();

        return Ok(markedFile);
    }
}