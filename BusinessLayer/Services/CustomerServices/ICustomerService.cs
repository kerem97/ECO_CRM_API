using DtoLayer.Customer.Requests;
using DtoLayer.Customer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<List<DisplayCustomerResponse>> GetAllCustomersAsync();
        Task<GetByIdCustomerResponse> GetCustomerByIdAsync(int id);
        Task AddCustomersAsync(AddCustomerRequest addCustomerRequest, int createdByUserId);
        Task UpdateCustomersAsync(UpdateCustomerRequest updateCustomerRequest, int updatedByUserId);
        Task DeleteCustomersAsync(int id);
        Task<(List<DisplayCustomerResponse>, int)> GetPagedCustomersAsync(int pageNumber, int pageSize);
        Task<List<string>> SearchCompaniesByName(string searchTerm, int pageNumber, int pageSize);
    }
}
