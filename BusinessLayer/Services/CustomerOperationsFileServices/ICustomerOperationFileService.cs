using DtoLayer.CustomerOperation.Responses;
using DtoLayer.CustomerOperationFile.Requests;
using DtoLayer.CustomerOperationFile.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.CustomerOperationsFileServices
{
    public interface ICustomerOperationFileService
    {
        Task AddFileAsync(AddCustomerOperationFileRequest request);
        Task<IEnumerable<DisplayCustomerOperationFileResponse>> GetFilesByOperationIdAsync(int operationId);
        Task DeleteFileAsync(int id);
    }
}
