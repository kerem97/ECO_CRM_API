﻿using BusinessLayer.Services.CustomerOperationServices;
using DtoLayer.CustomerOperation.Requests;
using DtoLayer.CustomerOperation.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CustomerOperationsController : ControllerBase
{
    private readonly ICustomerOperationService _customerOperationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomerOperationsController(ICustomerOperationService customerOperationService, IHttpContextAccessor httpContextAccessor)
    {
        _customerOperationService = customerOperationService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddOperation([FromBody] AddCustomerOperationRequest operationRequest)
    {
        var userId = GetCurrentUserId();
        await _customerOperationService.AddCustomerOperationsAsync(operationRequest, userId);

        return Ok("Operation added successfully");
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllOperations()
    {
        var operations = await _customerOperationService.GetAllCustomerOperationsAsync();
        return Ok(operations);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOperationById(int id)
    {
        var operation = await _customerOperationService.GetCustomerOperationByIdAsync(id);
        if (operation == null)
            return NotFound();

        return Ok(operation);
    }
    [HttpGet("by-customer/{customerId}")]
    public async Task<IActionResult> GetOperationsByCustomer(int customerId)
    {
        var operations = await _customerOperationService.GetOperationsByCustomerIdAsync(customerId);
        if (operations == null || !operations.Any())
            return NotFound();

        return Ok(operations);
    }
    [HttpGet("user-planned-operations")]
    public async Task<IActionResult> GetUserOperations(int pageNumber = 1, int pageSize = 10)
    {
        var userId = GetCurrentUserId();
        var (operations, totalOperations) = await _customerOperationService.GetUserPlannedOperationsAsync(userId, pageNumber, pageSize);

        Response.Headers.Add("X-Total-Count", totalOperations.ToString());

        return Ok(operations);
    }
    [HttpGet("user-complated-operations")]
    public async Task<IActionResult> GetUserComplatedOperations(int pageNumber = 1, int pageSize = 10)
    {
        var userId = GetCurrentUserId();
        var (operations, totalOperations) = await _customerOperationService.GetUserComplatedOperationsAsync(userId, pageNumber, pageSize);

        Response.Headers.Add("X-Total-Count", totalOperations.ToString());

        return Ok(operations);
    }
    [HttpGet("user-cancelled-operations")]
    public async Task<IActionResult> GetUserCancelledOperations(int pageNumber = 1, int pageSize = 10)
    {
        var userId = GetCurrentUserId();
        var (operations, totalOperations) = await _customerOperationService.GetUserCancelledOperationsAsync(userId, pageNumber, pageSize);

        Response.Headers.Add("X-Total-Count", totalOperations.ToString());

        return Ok(operations);
    }
    [HttpGet("all-operations")]
    public async Task<IActionResult> GetAllCustomerOperations(int pageNumber = 1, int pageSize = 10)
    {
        var operations = await _customerOperationService.GetAllCustomerOperationsPagedAsync(pageNumber, pageSize);

        if (operations == null || operations.Count == 0)
        {
            return NotFound("No operations found.");
        }

        return Ok(operations);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOperation(int id, [FromBody] UpdateCustomerOperationRequest operationRequest)
    {
        var userId = GetCurrentUserId();
        operationRequest.Id = id;
        await _customerOperationService.UpdateCustomerOperationsAsync(operationRequest, userId);

        return Ok("Operation updated successfully");
    }
    [HttpGet("paged-operations")]
    public async Task<IActionResult> GetPagedOperations(int pageNumber = 1, int pageSize = 15)
    {
        var operations = await _customerOperationService.GetPagedCustomerOperationsAsync(pageNumber, pageSize);
        return Ok(operations);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOperation(int id)
    {
        await _customerOperationService.DeleteCustomerOperationsAsync(id);
        return Ok("Operation deleted successfully");
    }
    [HttpPost("cancel-operation")]
    public async Task<IActionResult> CancelOperation([FromBody] CancelOperationRequest request)
    {
        try
        {
            await _customerOperationService.CancelOperationAsync(request.OperationId, request.CancelReason);
            return Ok(new { message = "Operation cancelled successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost("complete-operation")]
    public async Task<IActionResult> CompleteOperation([FromBody] CompleteOperationRequest request)
    {
        try
        {
            if (request.IsMeetingOnPlannedDate == null)
                return BadRequest(new { message = "Planned date meeting status is required." });

            await _customerOperationService.CompleteOperationAsync(request.OperationId, request.ActualDate, request.IsMeetingOnPlannedDate, request.UpdatedStatusDescription, request.OfferStatus, request.MeetingFeedback);
            return Ok(new { message = "Operation completed successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpGet("status-given-offers")]
    public async Task<IActionResult> GetPagedCustomerOperationsStatusGivenOffers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _customerOperationService.GetPagedCustomerOperationsStatusGivenOffers(pageNumber, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Sunucu hatası: {ex.Message}");
        }
    }
    [HttpPost("filtered-operations")]
    public async Task<IActionResult> GetFilteredOperations([FromBody] FilterCustomerOperationRequest filterRequest, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var filteredOperations = await _customerOperationService.GetFilteredOperationsAsync(filterRequest, pageNumber, pageSize);

            return Ok(filteredOperations);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpPost("filtered-user-operations")]
    public async Task<IActionResult> GetFilteredOperationsByUserId([FromBody] FilterCustomerOperationRequest filterRequest, int pageNumber = 1, int pageSize = 10)
    {
        var userId = GetCurrentUserId();
        var result = await _customerOperationService.GetFilteredOperationsByUserIdAsync(userId, filterRequest, pageNumber, pageSize);

        return Ok(result);
    }
    [HttpPost("filtered-planned-user-operations")]
    public async Task<IActionResult> GetPlannedFilteredOperationsByUserId([FromBody] FilterCustomerOperationRequest filterRequest, int pageNumber = 1, int pageSize = 10)
    {
        var userId = GetCurrentUserId();
        var result = await _customerOperationService.TGetPlannedFilteredOperationsByUserIdAsync(userId, filterRequest, pageNumber, pageSize);

        return Ok(result);
    }
    [HttpPost("filtered-complated-user-operations")]
    public async Task<IActionResult> GetComplatedFilteredOperationsByUserId([FromBody] FilterCustomerOperationRequest filterRequest, int pageNumber = 1, int pageSize = 10)
    {
        var userId = GetCurrentUserId();
        var result = await _customerOperationService.TGetComplatedFilteredOperationsByUserIdAsync(userId, filterRequest, pageNumber, pageSize);

        return Ok(result);
    }
    [HttpPost("filtered-cancelled-user-operations")]
    public async Task<IActionResult> GetCancelledFilteredOperationsByUserId([FromBody] FilterCustomerOperationRequest filterRequest, int pageNumber = 1, int pageSize = 10)
    {
        var userId = GetCurrentUserId();
        var result = await _customerOperationService.TGetCancelledFilteredOperationsByUserIdAsync(userId, filterRequest, pageNumber, pageSize);

        return Ok(result);
    }
    [HttpGet("get-dropdown-data")]
    public async Task<IActionResult> GetDropdownData()
    {
        var allOperations = await _customerOperationService.GetAllCustomerOperationsAsync();

        var companies = allOperations.Select(o => o.CustomerName).Distinct().ToList();
        var methods = allOperations.Select(o => o.Method).Distinct().ToList();
        var personnel = allOperations.Select(o => o.CreatedByUser).Distinct().ToList();
        var reasons = allOperations.Select(o => o.Reason).Distinct().ToList();

        return Ok(new { companies, methods, personnel, reasons });
    }
    [HttpGet("top-email-interactions")]
    public IActionResult GetTopEmailInteractions()
    {
        var result = _customerOperationService.GetTopEmailInteractions();
        return Ok(result);
    }
    [HttpGet("top-phone-interactions")]
    public IActionResult GetTopPhoneInteractions()
    {
        var result = _customerOperationService.GetTopPhoneInteractions();
        return Ok(result);
    }
    [HttpGet("top-face-to-face-interactions")]
    public IActionResult GetTopFaceToFaceInteractions()
    {
        var result = _customerOperationService.GetTopFaceToFaceInteractions();
        return Ok(result);
    }
    [HttpGet("user-email-interactions")]
    public async Task<IActionResult> GetUserEmailInteractions()
    {
        var userId = GetCurrentUserId();
        var result = await _customerOperationService.GetUserEmailInteractions(userId);
        return Ok(result);
    }
    [HttpGet("user-phone-interactions")]
    public async Task<IActionResult> GetUserPhoneInteractions()
    {
        var userId = GetCurrentUserId();
        var result = await _customerOperationService.GetUserPhoneInteractions(userId);
        return Ok(result);
    }
    [HttpGet("user-face-to-face-interactions")]
    public async Task<IActionResult> GetUserFaceToFaceInteractions()
    {
        var userId = GetCurrentUserId();
        var result = await _customerOperationService.GetUserFaceToFaceInteractions(userId);
        return Ok(result);
    }
    [HttpGet("total-operation-stats")]
    public async Task<IActionResult> GetTotalOperationStats()
    {
        var result = await _customerOperationService.GetTotalOperationStatsAsync();
        return Ok(result);
    }
    [HttpGet("user-operation-stats")]
    public async Task<IActionResult> GetUserOperationStats()
    {
        var userId = GetCurrentUserId();
        var result = await _customerOperationService.GetUserOperationStatsAsync(userId);
        return Ok(result);
    }
    [HttpGet("customer/{customerId}/total-operations")]
    public async Task<IActionResult> GetTotalOperationsByCustomerId(int customerId)
    {
        var totalOperations = await _customerOperationService.TGetTotalOperationsByCustomerIdAsync(customerId);
        return Ok(totalOperations);
    }
    private int GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        return int.Parse(userIdClaim.Value);
    }

    [HttpGet("last-visit-user/{customerId}")]
    public async Task<IActionResult> GetLastVisitUser(int customerId)
    {
        var lastVisitUserDto = await _customerOperationService.TGetLastVisitUserByCustomerIdAsync(customerId);

        if (lastVisitUserDto == null)
        {
            return NotFound("No visit found for this customer.");
        }

        return Ok(lastVisitUserDto);
    }
    [HttpGet("customer/{customerId}/last-10-operations")]
    public async Task<IActionResult> GetLast10CustomerOperations(int customerId)
    {
        var operations = await _customerOperationService.GetLast10CustomerOperationsByCustomerIdAsync(customerId);

        if (operations == null || !operations.Any())
        {
            return NotFound("No operations found for this customer.");
        }

        return Ok(operations);
    }
}
