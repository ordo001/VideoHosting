using Microsoft.AspNetCore.Mvc;
using VideoHostingApi.FileService.Service.Contracts;

namespace VideoHostingApi.FileService.Web.Controllers;

[Route("[controller]")]
[ApiController]
public class VideoController(IVideoService videoService) : ControllerBase
{

    [HttpPost("Upload")]
    public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
    {
        var stream = file.OpenReadStream();
        await videoService.UploadFile(file.FileName, stream, file.ContentType, cancellationToken);
        return Ok();
    }
    
    [HttpGet("Download")]
    public async Task<IActionResult> DownloadFile(string name, CancellationToken cancellationToken)
    {
        var result = await videoService.DownloadFile(name, cancellationToken);
        return File(result.FileStream, result.ContentType, name);
    }

    [HttpGet("GetPresignedDownloadUrl")]
    public async Task<IActionResult> GetPresignedDownloadUrl(string name, CancellationToken cancellationToken)
    {
        var result = await videoService.GetPresignedDownloadUrl(name, cancellationToken);
        return Ok(result);
    }

    [HttpGet("GetUploadUrl")]
    public async Task<IActionResult> GetUploadUrl(string fileName, CancellationToken cancellationToken)
    {
        var url = await videoService.GetPresignedUploadUrl(fileName, cancellationToken);
        return Ok(url);
    }
    
    [HttpGet("GetDownloadUrl")]
    public async Task<IActionResult> GetDownloadUrl(string fileName, CancellationToken cancellationToken)
    {
        var url = await videoService.GetPresignedDownloadUrl(fileName, cancellationToken);
        return Ok(url);
    }
    
}