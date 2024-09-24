using BusinessLayer.Services.CustomerOperationServices;
using DtoLayer.CustomerOperation.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CustomerOperationController : ControllerBase
{
    private readonly ICustomerOperationService _customerOperationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomerOperationController(ICustomerOperationService customerOperationService, IHttpContextAccessor httpContextAccessor)
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
        return Ok(operations);
    }
    [HttpGet("user-operations")]
    public async Task<IActionResult> GetUserOperations()
    {
        var userId = GetCurrentUserId(); 
        var operations = await _customerOperationService.GetUserOperationsAsync(userId);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOperation(int id)
    {
        await _customerOperationService.DeleteCustomerOperationsAsync(id);
        return Ok("Operation deleted successfully");
    }

    private int GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        return int.Parse(userIdClaim.Value);
    }
}
