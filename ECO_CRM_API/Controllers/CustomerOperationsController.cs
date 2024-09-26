using BusinessLayer.Services.CustomerOperationServices;
using DtoLayer.CustomerOperation.Requests;
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
    [HttpGet("user-operations")]
    public async Task<IActionResult> GetUserOperations(int pageNumber = 1, int pageSize = 10)
    {
        var userId = GetCurrentUserId();
        var (operations, totalOperations) = await _customerOperationService.GetUserOperationsAsync(userId, pageNumber, pageSize);

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

            await _customerOperationService.CompleteOperationAsync(request.OperationId, request.ActualDate, request.IsMeetingOnPlannedDate, request.UpdatedStatusDescription);
            return Ok(new { message = "Operation completed successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost("filtered-operations")]
    public async Task<IActionResult> GetFilteredOperations([FromBody] FilterCustomerOperationRequest filterRequest)
    {
        try
        {
            var operations = await _customerOperationService.GetFilteredOperationsAsync(filterRequest);
            return Ok(operations);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
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


    private int GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        return int.Parse(userIdClaim.Value);
    }
}
