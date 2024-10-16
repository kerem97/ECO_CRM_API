using DataAccessLayer.Repository;
using DtoLayer.CustomerOperation.Responses;
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
        Task<(List<CustomerOperation>, int)> GetPlannedOperationsByUserId(int userId, int pageNumber, int pageSize);
        Task<(List<CustomerOperation>, int)> GetComplatedOperationsByUserId(int userId, int pageNumber, int pageSize);
        Task<(List<CustomerOperation>, int)> GetCanceledOperationsByUserId(int userId, int pageNumber, int pageSize);
        Task<List<CustomerOperation>> GetPagedCustomerOperationsAsync(int pageNumber, int pageSize);
        Task<List<CustomerOperation>> GetPagedCustomerOperationsStatusGivenOffers(int pageNumber, int pageSize);
        Task<List<CustomerOperation>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<List<CustomerOperation>> GetFilteredOperationsAsync(string companyName, int? month, int? year, string method, string performedBy, string reason, string status, int pageNumber, int pageSize);
        Task<List<CustomerOperation>> GetFilteredOperationsByUserIdAsync(int userId, string companyName, int? month, int? year, string method, string performedBy, string reason, string status, int pageNumber, int pageSize);
        Task<List<CustomerOperation>> GetPlannedFilteredOperationsByUserIdAsync(int userId, string companyName, int? month, int? year, string method, string performedBy, string reason, string status, int pageNumber, int pageSize);
        Task<List<CustomerOperation>> GetComplatedFilteredOperationsByUserIdAsync(int userId, string companyName, int? month, int? year, string method, string performedBy, string reason, string status, int pageNumber, int pageSize);
        Task<List<CustomerOperation>> GetCancelledFilteredOperationsByUserIdAsync(int userId, string companyName, int? month, int? year, string method, string performedBy, string reason, string status, int pageNumber, int pageSize);
        Task<List<(string companyName, int count)>> GetTopEmailInteractions();
        Task<List<(string companyName, int count)>> GetTopFaceToFaceInteractions();
        Task<List<(string companyName, int count)>> GetTopPhoneInteractions();
        Task<List<(string companyName, int customerId, int count)>> GetUserEmailInteractions(int userId);
        Task<List<(string companyName, int customerId, int count)>> GetUserPhoneInteractions(int userId);
        Task<List<(string companyName, int customerId, int count)>> GetUserFaceToFaceInteractions(int userId);
        Task<(int plannedCount, int completedCount)> GetTotalOperationStatsAsync();
        Task<(int plannedCount, int completedCount)> GetUserOperationStatsAsync(int userId);
        Task<int> GetTotalOperationsByCustomerIdAsync(int customerId);
        Task<GetCustomerLastVisitUserResponse> GetLastVisitUserNameByCustomerIdAsync(int customerId);
        Task<List<GetByCustomerIdLast10OperationsResponse>> GetLast10CustomerOperationsByCustomerIdAsync(int customerId);
    }
}
