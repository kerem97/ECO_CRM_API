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
        Task AddCustomerOperationsAsync(AddCustomerOperationRequest addCustomerOperationRequest);
        Task UpdateCustomerOperationsAsync(UpdateCustomerOperationRequest updateCustomerOperationRequest);
        Task DeleteCustomerOperationsAsync(int id);
    }
}
