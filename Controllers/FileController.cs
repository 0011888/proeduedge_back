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

        [HttpGet, Route("/{containerName}")]
        public async Task<IActionResult> ListAllBlobs(string containerName)
        {
            try
            {
                var result = await _fileService.ListAsync(containerName);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost, Route("upload/{containerName}")]
        public async Task<IActionResult> Upload(IFormFile file, string containerName)
        {
            try
            {
                var result = await _fileService.UploadAsync(containerName, file);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "An error occurred while uploading the file.");
            }
        }


        [HttpPost, Route("uploadMultiple/{containerName}")]
        public async Task<IActionResult> UploadMultiple(IEnumerable<IFormFile> files, string containerName)
        {
            try
            {
                var result = await _fileService.UploadMultipleAsync(containerName, files);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "An error occurred while uploading the files.");
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
