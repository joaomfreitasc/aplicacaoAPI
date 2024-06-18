using Microsoft.AspNetCore.Mvc;

namespace ComputerVisionAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComputerVisionController : ControllerBase
{
  private readonly AzureComputerVisionService _azureComputerVisionService;

  public ComputerVisionController(AzureComputerVisionService azureComputerVisionService)
  {
    _azureComputerVisionService = azureComputerVisionService;
  }


  [HttpPost("teste")]
  public async Task<IActionResult> AnalyzeImage(string file)
  {
    return Ok(file);
  }


  [HttpPost("analyze")]
  public async Task<IActionResult> AnalyzeImage([FromForm] IFormFile file)
  {
    if (file == null || file.Length == 0)
    {
      return BadRequest("Nenhum arquivo foi enviado.");
    }

    using var memoryStream = new MemoryStream();
    await file.CopyToAsync(memoryStream);
    var imageBytes = memoryStream.ToArray();

    var analysisResult = await _azureComputerVisionService.AnalyzeImageAsync(imageBytes);
    return Ok(analysisResult);
  }
}
