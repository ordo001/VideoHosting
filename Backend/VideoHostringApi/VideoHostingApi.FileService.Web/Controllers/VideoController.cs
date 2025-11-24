using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoHostingApi.FileService.Service.Contracts;
using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Web.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class VideoController(IVideoService fileService, IMapper mapper) : ControllerBase
{
    
    [HttpGet("upload-url")]
    public async Task<IActionResult> GetUploadUrl(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var model = await fileService.GetPresignedUploadUrl(userId, cancellationToken);
        return Ok(model);
    }

    [HttpPost("complete-upload/{videoId:guid}")]
    public async Task<IActionResult> CompleteUpload(Guid videoId ,CancellationToken cancellationToken)
    {
        await fileService.UploadCompete(Guid.NewGuid(), cancellationToken);
        return Ok();
    }
    
    [HttpGet("download-url")]
    public async Task<IActionResult> GetDownloadUrl(string fileName, CancellationToken cancellationToken)
    {
        var url = await fileService.GetPresignedDownloadUrl(fileName, cancellationToken);
        return Ok(url);
    }
    
}