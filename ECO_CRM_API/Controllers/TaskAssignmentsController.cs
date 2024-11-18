using BusinessLayer.Services.CustomerOperationsFileServices;
using BusinessLayer.Services.TaskAssignmentServices;
using DtoLayer.CustomerOperationFile.Responses;
using DtoLayer.TaskAssignment.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECO_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentsController : ControllerBase
    {
        private readonly ITaskAssignmentService _taskAssignmentService;
        private readonly ICustomerOperationFileService _customerOperationFileService;
        public TaskAssignmentsController(ITaskAssignmentService taskAssignmentService, ICustomerOperationFileService customerOperationFileService)
        {
            _taskAssignmentService = taskAssignmentService;
            _customerOperationFileService = customerOperationFileService;
        }
        [HttpPost("taskassignment")]
        public async Task<IActionResult> AddTaskAssignment([FromForm] AddTaskAssignmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskAssignmentService.AddTaskAssignmentAsync(request);
            return Ok("Görev başarıyla eklendi.");
        }
        [HttpPut("taskassignment/{id}")]
        public async Task<IActionResult> UpdateTaskAssignment(int id, [FromBody] UpdateTaskAssignmentRequest request)
        {
            await _taskAssignmentService.UpdateTaskAssignmentAsync(id, request);
            return NoContent();
        }
        [HttpDelete("taskassignment/{id}")]
        public async Task<IActionResult> DeleteTaskAssignment(int id)
        {
            await _taskAssignmentService.DeleteTaskAssignmentAsync(id);
            return Ok("Görev başarıyla silindi.");
        }
        [HttpGet("taskassignment")]
        public async Task<IActionResult> GetAllTaskAssignments()
        {
            var result = await _taskAssignmentService.GetAllTaskAssignmentAsync();
            return Ok(result);
        }
        [HttpGet("taskassignment/{id}")]
        public async Task<IActionResult> GetTaskAssignmentById(int id)
        {
            var task = await _taskAssignmentService.GetTaskAssignmentByIdAsync(id);
            if (task == null)
                return NotFound("Görev bulunamadı.");

            var operationId = task.OperationId;
            var files = await _customerOperationFileService.GetFilesByOperationIdAsync(operationId);

            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var mappedFiles = files.Select(file => new DisplayCustomerOperationFileResponse
            {
                Id = file.Id,
                FilePath = $"{baseUrl}/uploads/customeroperations/{file.FileName}",
                FileName = file.FileName,
                UploadedDate = file.UploadedDate
            }).ToList();

            task.Files = mappedFiles;

            return Ok(task);
        }



        [HttpGet("pending-tasks")]
        public async Task<IActionResult> GetPendingTasks(int pageNumber = 1, int pageSize = 10)
        {
            var tasks = await _taskAssignmentService.TGetPendingTasksAsync(pageNumber, pageSize);
            return Ok(tasks);
        }
        [HttpGet("proposal-given-tasks")]
        public async Task<IActionResult> GetProposalGivenTasks(int pageNumber = 1, int pageSize = 10)
        {
            var tasks = await _taskAssignmentService.TGetProposalGivenTasksAsync(pageNumber, pageSize);
            return Ok(tasks);
        }
        [HttpPost("task/update-status-to-givenoffer")]
        public async Task<IActionResult> UpdateTaskStatusToOfferGiven([FromBody] UpdateTaskAssignmentStatusToOfferGivenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskAssignmentService.UpdateTaskStatusToOfferGivenAsync(request);
            return Ok("Görev durumu başarıyla güncellendi.");
        }
        [HttpPost("task/update-status-to-givenproposal")]
        public async Task<IActionResult> UpdateTaskStatusToProposalGiven([FromBody] UpdateTaskStatusToProposalGivenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskAssignmentService.UpdateTaskStatusToProposalGivenAsync(request);
            return Ok("Görev durumu başarıyla güncellendi.");
        }
        [HttpPost("task/update-status-to-approved")]
        public async Task<IActionResult> UpdateTaskStatusToApproved([FromBody] UpdateTaskStatusToApprovedRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskAssignmentService.UpdateTaskStatusToApprovedAsync(request);
            return Ok("Görev durumu başarıyla Onaylandı olarak güncellendi.");
        }

        [HttpPost("task/update-status-to-rejected")]
        public async Task<IActionResult> UpdateTaskStatusToRejected([FromBody] UpdateTaskStatusToRejectedRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskAssignmentService.UpdateTaskStatusToRejectedAsync(request);
            return Ok("Görev durumu başarıyla Reddedildi olarak güncellendi.");
        }
        [HttpGet("completed-tasks")]
        public async Task<IActionResult> GetCompletedTasks(int pageNumber = 1, int pageSize = 10)
        {
            var tasks = await _taskAssignmentService.TGetCompletedTasksAsync(pageNumber, pageSize);
            return Ok(tasks);
        }
        [HttpGet("customer/{customerId}/approved-task-count")]
        public async Task<IActionResult> GetApprovedTaskCountByCustomerId(int customerId)
        {
            var result = await _taskAssignmentService.TGetApprovedTaskCountByCustomerIdAsync(customerId);
            return Ok(result);
        }
        [HttpGet("customer/{customerId}/not-approved-task-count")]
        public async Task<IActionResult> GetNotApprovedTaskCountByCustomerId(int customerId)
        {
            var result = await _taskAssignmentService.TGetNotApprovedTaskCountByCustomerIdAsync(customerId);
            return Ok(result);
        }
        [HttpGet("customer/{customerId}/task-assignment-count")]
        public async Task<IActionResult> GetTaskAssignmentCount(int customerId)
        {
            var taskCountDto = await _taskAssignmentService.GetTaskAssignmentCountAsync(customerId);
            return Ok(taskCountDto);
        }
        [HttpGet("customer/{customerId}/last-10-task-assignments")]
        public async Task<IActionResult> GetLast10TaskAssignmentsByCustomerId(int customerId)
        {
            var taskAssignments = await _taskAssignmentService.TGetLast10TaskAssignmentsByCustomerIdAsync(customerId);

            if (taskAssignments == null || !taskAssignments.Any())
            {
                return NotFound("No task assignments found for this customer.");
            }

            return Ok(taskAssignments);
        }
    }
}
