using AutoMapper;
using MarkingService.Entities;
using MarkingService.Services;
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
    public async Task<ActionResult<MarkedFileDto>> MarkFile(UnmarkedFileDto unmarkedFileDto)
    {
        var unmarkedFile = _mapper.Map<UnmarkedFile>(unmarkedFileDto);
        var markedFile = _fileMarkingService.MarkFile(unmarkedFile);
        return Ok(markedFile);
    }
}

public class UnmarkedFileDto
{
    // note: nothing comes in if it hasn't first been vetted by the upload service
    public string FileLocation { get; set; }
    public string FileType { get; set; } // ex. application/pdf
    public ClassificationTier ClassificationTier { get; set; }
}

public class MarkedFileDto
{
    public bool Success { get; set; }
}