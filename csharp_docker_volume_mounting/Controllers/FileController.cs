using csharp_docker_volume_mounting.Models;
using Microsoft.AspNetCore.Mvc;

namespace csharp_docker_volume_mounting.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    [HttpPost("UploadFile")]
    public async Task<IActionResult> UploadFileAsync(
        FileRequestModel requestModel,
        CancellationToken cs
    )
    {
        string fileName =
            $"{DateTime.UtcNow.ToString("yyyyMMdd_HHmmssfff")}_{requestModel.File.FileName}";
        string filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "Uploads",
            fileName
        );

        await using var stream = new FileStream(filePath, FileMode.Create);
        await requestModel.File.CopyToAsync(stream, cs);

        return Ok("Uploading Successful.");
    }
}
