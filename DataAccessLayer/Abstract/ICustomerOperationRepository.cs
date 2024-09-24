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
        public Task<List<CustomerOperation>> GetOperationsByUserId(int userId);
    }
}
