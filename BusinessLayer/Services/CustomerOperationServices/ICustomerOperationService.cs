using DtoLayer.Customer.Responses;
using DtoLayer.CustomerOperation.Requests;
using DtoLayer.CustomerOperation.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.CustomerOperationServices
{
    public interface ICustomerOperationService
    {
        Task<List<DisplayCustomerOperationResponse>> GetAllCustomerOperationsAsync();
        Task<GetByIdCustomerOperationResponse> GetCustomerOperationByIdAsync(int id);
        Task<List<DisplayCustomerOperationByCustomerResponse>> GetOperationsByCustomerIdAsync(int customerId);
        Task<List<DisplayCustomerOperationResponse>> GetUserOperationsAsync(int userId);
        Task AddCustomerOperationsAsync(AddCustomerOperationRequest addCustomerOperationRequest, int userId);
        Task UpdateCustomerOperationsAsync(UpdateCustomerOperationRequest updateCustomerOperationRequest, int userId);
        Task DeleteCustomerOperationsAsync(int id);
    }
}
