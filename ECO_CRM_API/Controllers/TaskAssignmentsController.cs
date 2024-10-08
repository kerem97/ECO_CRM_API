﻿using BusinessLayer.Services.TaskAssignmentServices;
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

        public TaskAssignmentsController(ITaskAssignmentService taskAssignmentService)
        {
            _taskAssignmentService = taskAssignmentService;
        }
        [HttpPost("taskassignment")]
        public async Task<IActionResult> AddTaskAssignment([FromBody] AddTaskAssignmentRequest request)
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
            var result = await _taskAssignmentService.GetUserByIdAsync(id);
            if (result == null)
                return NotFound("Görev bulunamadı.");

            return Ok(result);
        }



    }
}
