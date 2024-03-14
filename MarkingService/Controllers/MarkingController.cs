using AutoMapper;
using MarkingService.Models.Dto;
using MarkingService.Services;
using MarkingService.Services.Builders;
using Microsoft.AspNetCore.Mvc;

namespace MarkingService.Controllers;

[ApiController]
[Route("/api/mark")]
public class MarkingController : ControllerBase
{
    private readonly IFileMarkingService _fileMarkingService;
    private readonly IMapper _mapper;
    private readonly ILogger<MarkingController> _logger;

    public MarkingController(IFileMarkingService fileMarkingService, IMapper mapper, ILogger<MarkingController> logger)
    {
        _fileMarkingService = fileMarkingService ?? throw new ArgumentNullException(nameof(fileMarkingService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
        
        // todo: ensure mapping is functional
        var unmarkedFile = unmarkedFileBuilder
            .WithDtoData(unmarkedFileDto)
            .Build();
        
        // todo: ensure successful file marking
        var markedFile = _fileMarkingService.MarkFile(unmarkedFile);
        // todo: refactor to created at route
        return Ok(markedFile);
    }
}