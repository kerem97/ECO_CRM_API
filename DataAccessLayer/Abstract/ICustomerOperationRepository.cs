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
        public Task<List<CustomerOperation>> GetOperationsByCustomerId(int customerId);
        Task<(List<CustomerOperation>, int)> GetOperationsByUserId(int userId, int pageNumber, int pageSize);
        public Task<List<CustomerOperation>> GetPagedCustomerOperationsAsync(int pageNumber, int pageSize);
        public Task<List<CustomerOperation>> GetAllPagedAsync(int pageNumber, int pageSize);
    }
}
