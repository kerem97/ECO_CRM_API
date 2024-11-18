using BusinessLayer.Services.CustomerOperationsFileServices;
using BusinessLayer.Services.TaskAssignmentServices;
using DtoLayer.CustomerOperationFile.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECO_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOperationFilesController : ControllerBase
    {
        private readonly ITaskAssignmentService _taskAssignmentService;
        private readonly ICustomerOperationFileService _operationFileService;

        public CustomerOperationFilesController(
            ITaskAssignmentService taskAssignmentService,
            ICustomerOperationFileService operationFileService)
        {
            _taskAssignmentService = taskAssignmentService;
            _operationFileService = operationFileService;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] AddCustomerOperationFileRequest request)
        {
            await _operationFileService.AddFileAsync(request);
            return Ok("Dosya başarıyla yüklendi.");
        }
        [HttpGet("get-files-by-task/{taskAssignmentId}")]
        public async Task<IActionResult> GetFilesByTask(int taskAssignmentId)
        {
            // TaskAssignment kontrolü
            var task = await _taskAssignmentService.GetTaskAssignmentByIdAsync(taskAssignmentId);
            if (task == null)
            {
                return NotFound("TaskAssignment bulunamadı.");
            }

            // OperationId üzerinden dosyaları al
            var files = await _operationFileService.GetFilesByOperationIdAsync(task.Id);
            if (files == null || !files.Any())
            {
                return NotFound("Bu görev için dosya bulunamadı.");
            }

            return Ok(files);
        }
        [HttpGet("{operationId}/files")]
        public async Task<IActionResult> GetFiles(int operationId)
        {
            var files = await _operationFileService.GetFilesByOperationIdAsync(operationId);
            return Ok(files);
        }
        [HttpGet("download/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            var filePath = Path.Combine("wwwroot", "uploads", "customeroperations", fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Dosya bulunamadı.");

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            await _operationFileService.DeleteFileAsync(id);
            return Ok("Dosya başarıyla silindi.");
        }

    }
}
