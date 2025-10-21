using Microsoft.AspNetCore.Mvc;
using VideoHostingApi.FileService.Service.Contracts;

namespace VideoHostingApi.FileService.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoController(IVideoService videoService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Test()
    {
        await videoService.DownloadFile("a");
                
        return Ok();
    }
}