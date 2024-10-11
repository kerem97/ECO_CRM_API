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
                return BadRequest("Dosya seçiniz.");
            }

            await _taskAssignmentFileService.AddTaskAssignmentFile(request);
            return Ok("Dosya başarıyla yüklendi.");
        }
    }
}
