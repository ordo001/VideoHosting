using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoHostingApi.FileService.Service.Contracts;
using VideoHostingApi.FileService.Service.Contracts.Models;
using VideoHostingApi.FileService.Web.Models;

namespace VideoHostingApi.FileService.Web.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class VideoController(IVideoService fileService, IMapper mapper) : ControllerBase
{
    
    [HttpGet("upload-url")]
    public async Task<IActionResult> GetUploadUrl(UploadVideoRequest uploadVideoRequest, CancellationToken cancellationToken)
    {
        var uploadModel = mapper.Map<UploadVideoModel>(uploadVideoRequest);
        uploadModel.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var model = await fileService.GetPresignedUploadUrl(uploadModel, cancellationToken);
        return Ok(model);
    }

    [HttpPost("complete-upload/{videoId:guid}")]
    public async Task<IActionResult> CompleteUpload(Guid videoId ,CancellationToken cancellationToken)
    {
        await fileService.UploadCompete(videoId, cancellationToken);
        return Ok();
    }
    
    [HttpGet("download-url/{videoId:guid}")]
    public async Task<IActionResult> GetDownloadUrl(Guid videoId, CancellationToken cancellationToken)
    {
        var url = await fileService.GetPresignedDownloadUrl(videoId, cancellationToken);
        return Ok(url);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(UploadFileVideoRequest uploadFileVideoRequest, CancellationToken cancellationToken)
    {
        var model = mapper.Map<AddFileModel>(uploadFileVideoRequest);
        model.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        model.FileStream = uploadFileVideoRequest.VideoFile.OpenReadStream();
        model.ContentType = uploadFileVideoRequest.VideoFile.ContentType;
        
        await  fileService.UploadFile(model, cancellationToken);
        return Ok();
    }
    
}