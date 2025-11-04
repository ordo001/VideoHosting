using Microsoft.AspNetCore.Mvc;
using VideoHostingApi.FileService.Service.Contracts;

namespace VideoHostingApi.FileService.Web.Controllers;

[Route("[controller]")]
[ApiController]
public class VideoController(IVideoService fileService) : ControllerBase
{

    [HttpPost("Upload")]
    public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
    {
        var stream = file.OpenReadStream();
        await fileService.UploadFile(file.FileName, stream, file.ContentType, cancellationToken);
        return Ok();
    }
    
    [HttpGet("Download")]
    public async Task<IActionResult> DownloadFile(string name, CancellationToken cancellationToken)
    {
        var result = await fileService.DownloadFile(name, cancellationToken);
        return File(result.FileStream, result.ContentType, name);
    }

    [HttpGet("GetUploadUrl")]
    public async Task<IActionResult> GetUploadUrl(string fileName, CancellationToken cancellationToken)
    {
        var url = await fileService.GetPresignedUploadUrl(fileName, cancellationToken);
        return Ok(url);
    }
    
    [HttpGet("GetDownloadUrl")]
    public async Task<IActionResult> GetDownloadUrl(string fileName, CancellationToken cancellationToken)
    {
        var url = await fileService.GetPresignedDownloadUrl(fileName, cancellationToken);
        return Ok(url);
    }
    
    [HttpGet("All")]
    public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
    {
        var result = await fileService.GetListObjects(cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(string fileName, CancellationToken cancellationToken)
    {
        await fileService.DeleteFile(fileName, cancellationToken);
        return Ok();
    }
    
}