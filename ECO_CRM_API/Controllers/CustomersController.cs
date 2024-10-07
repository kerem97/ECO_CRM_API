using BusinessLayer.Services.CustomerServices;
using BusinessLayer.Services.UserServices;
using DtoLayer.Customer.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECO_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomersController(ICustomerService customerService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _customerService = customerService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userId);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }
        [HttpGet("paged-customers")]
        public async Task<IActionResult> GetPagedCustomers(int pageNumber = 1, int pageSize = 15)
        {
            var (customers, totalRecords) = await _customerService.GetPagedCustomersAsync(pageNumber, pageSize);

            Response.Headers.Add("X-Total-Count", totalRecords.ToString());

            return Ok(customers);
        }
        [HttpGet("paged-existed-customers")]
        public async Task<IActionResult> GetPagedExistedCustomers(int pageNumber = 1, int pageSize = 15)
        {
            var (customers, totalRecords) = await _customerService.TGetAllExistedCustomersPaged(pageNumber, pageSize);

            Response.Headers.Add("X-Total-Count", totalRecords.ToString());

            return Ok(customers);
        }
        [HttpGet("paged-potential-customers")]
        public async Task<IActionResult> GetPagedPotentialCustomers(int pageNumber = 1, int pageSize = 15)
        {
            var (customers, totalRecords) = await _customerService.TGetAllPotentialCustomersPaged(pageNumber, pageSize);

            Response.Headers.Add("X-Total-Count", totalRecords.ToString());

            return Ok(customers);
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerRequest customerDto)
        {
            var userId = GetCurrentUserId();

            await _customerService.AddCustomersAsync(customerDto, userId);

            return Ok("Customer added successfully");
        }
        [HttpGet("search-companies")]
        public async Task<IActionResult> SearchCompanies([FromQuery] string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            var customerDtos = await _customerService.SearchCompaniesByName(searchTerm, pageNumber, pageSize);
            return Ok(customerDtos);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerRequest customerDto)
        {
            customerDto.Id = id;

            var userId = GetCurrentUserId();

            await _customerService.UpdateCustomersAsync(customerDto, userId);

            return Ok("Customer updated successfully");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomersAsync(id);
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customerDto = await _customerService.GetCustomerByIdAsync(id);

            if (customerDto == null)
                return NotFound();
            return Ok(customerDto);
        }

    }
}
