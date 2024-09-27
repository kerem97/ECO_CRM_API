using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerOperationRepository : IRepository<CustomerOperation>
    {
        Task<List<CustomerOperation>> GetOperationsByCustomerId(int customerId);
        Task<(List<CustomerOperation>, int)> GetOperationsByUserId(int userId, int pageNumber, int pageSize);
        Task<List<CustomerOperation>> GetPagedCustomerOperationsAsync(int pageNumber, int pageSize);
        Task<List<CustomerOperation>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<List<CustomerOperation>> GetFilteredOperationsAsync(string companyName, int? month, int? year, string method, string performedBy, string reason, string status, int pageNumber, int pageSize);

    }
}
