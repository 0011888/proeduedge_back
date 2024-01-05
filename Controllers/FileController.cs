using Microsoft.AspNetCore.Mvc;
using proeduedge.Services;

namespace proeduedge.Controllers
{
    [Route("api/proeduedge")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAllBlobs()
        {
            try
            {
                var result = await _fileService.ListAsync("avatar");
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost, Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                var result = await _fileService.UploadAsync("avatar", file);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "An error occurred while uploading the file.");
            }
        }

        [HttpGet, Route("download/{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            try
            {
                var result = await _fileService.DownloadAsync("avatar", filename);
                if (result == null)
                {
                    return NotFound("File not found.");
                }
                return File(result.Content, result.ContentType, result.Name);
            }
            catch (System.Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "An error occurred while downloading the file.");
            }
        }

        [HttpDelete, Route("delete/{filename}")]
        public async Task<IActionResult> Delete(string filename)
        {
            try
            {
                var result = await _fileService.DeleteAsync("avatar", filename);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "An error occurred while deleting the file.");
            }
        }
    }
}
