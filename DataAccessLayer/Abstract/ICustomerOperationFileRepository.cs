using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerOperationFileRepository : IRepository<CustomerOperationFile>
    {
        Task<IEnumerable<CustomerOperationFile>> GetFilesByOperationIdAsync(int operationId);
    }
}
