using BusinessLayer.Services.TaskAssignmentFileServices;
using DtoLayer.TaskAssignmentFile.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECO_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentFilesController : ControllerBase
    {
        private readonly ITaskAssignmentFileService _taskAssignmentFileService;

        public TaskAssignmentFilesController(ITaskAssignmentFileService taskAssignmentFileService)
        {
            _taskAssignmentFileService = taskAssignmentFileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadTaskAssignmentFile([FromForm] AddTaskAssignmentFileRequest request)
        {
            if (request.File == null || request.File.Length == 0)
            {
                return BadRequest("Lütfen bir dosya seçiniz.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var fileExtension = Path.GetExtension(request.File.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Sadece PDF ve görsel dosyalar yüklenebilir.");
            }

            const long maxFileSize = 5 * 1024 * 1024; // 5 MB
            if (request.File.Length > maxFileSize)
            {
                return BadRequest("Dosya boyutu 5 MB'yi geçemez.");
            }

            await _taskAssignmentFileService.AddTaskAssignmentFile(request);

            return Ok(new { message = "Dosya başarıyla yüklendi." });
        }

    }
}
