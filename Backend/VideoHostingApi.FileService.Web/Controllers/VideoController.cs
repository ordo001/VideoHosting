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

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
    {
        var model = mapper.Map<AddFileModel>(file);
        model.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        
        await fileService.UploadFile(model, cancellationToken);
        return Ok();
    }
    
    [HttpGet("download")]
    public async Task<IActionResult> DownloadFile(string name, CancellationToken cancellationToken)
    {
        var result = await fileService.DownloadFile(name, cancellationToken);
        return File(result.FileStream, result.ContentType, name);
    }

    [HttpGet("upload-url")]
    public async Task<IActionResult> GetUploadUrl(string fileName, CancellationToken cancellationToken)
    {
        var url = await fileService.GetPresignedUploadUrl(fileName, cancellationToken);
        return Ok(url);
    }
    
    [HttpGet("download-url")]
    public async Task<IActionResult> GetDownloadUrl(string fileName, CancellationToken cancellationToken)
    {
        var url = await fileService.GetPresignedDownloadUrl(fileName, cancellationToken);
        return Ok(url);
    }
    
    [HttpGet("metadata")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await fileService.GetAllMetadata(cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("{fileName}/metadata")]
    public async Task<IActionResult> GetMetadataByName(string fileName, CancellationToken cancellationToken)
    {
        var result = await fileService.GetMetadata(fileName, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(string fileName, CancellationToken cancellationToken)
    {
        await fileService.DeleteFile(fileName, cancellationToken);
        return Ok();
    }
    
}