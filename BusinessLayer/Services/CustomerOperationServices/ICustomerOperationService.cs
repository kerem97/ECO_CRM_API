using DtoLayer.Customer.Responses;
using DtoLayer.CustomerOperation.Requests;
using DtoLayer.CustomerOperation.Responses;
using EntityLayer.Concrete;
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
        Task<(List<DisplayCustomerOperationResponse>, int)> GetUserPlannedOperationsAsync(int userId, int pageNumber, int pageSize);
        Task<(List<DisplayCustomerOperationResponse>, int)> GetUserComplatedOperationsAsync(int userId, int pageNumber, int pageSize);
        Task<(List<DisplayCustomerOperationResponse>, int)> GetUserCancelledOperationsAsync(int userId, int pageNumber, int pageSize);
        Task AddCustomerOperationsAsync(AddCustomerOperationRequest addCustomerOperationRequest, int userId);
        Task UpdateCustomerOperationsAsync(UpdateCustomerOperationRequest updateCustomerOperationRequest, int userId);
        Task DeleteCustomerOperationsAsync(int id);
        Task<List<DisplayCustomerOperationResponse>> GetPagedCustomerOperationsAsync(int pageNumber, int pageSize);
        Task<List<DisplayCustomerOperationResponse>> GetAllCustomerOperationsPagedAsync(int pageNumber, int pageSize);
        Task CancelOperationAsync(int operationId, string cancelReason);
        Task CompleteOperationAsync(int operationId, DateTime actualDate, bool? isMeetingOnPlannedDate, string updatedStatusDescription, string offerStatus);
        Task<List<DisplayCustomerOperationResponse>> GetFilteredOperationsAsync(FilterCustomerOperationRequest filterRequest, int pageNumber, int pageSize);
        Task<List<DisplayCustomerOperationResponse>> GetFilteredOperationsByUserIdAsync(int userId, FilterCustomerOperationRequest filterRequest, int pageNumber, int pageSize);
        Task<List<DisplayCustomerOperationResponse>> TGetPlannedFilteredOperationsByUserIdAsync(int userId, FilterCustomerOperationRequest filterRequest, int pageNumber, int pageSize);
        Task<List<DisplayCustomerOperationResponse>> TGetComplatedFilteredOperationsByUserIdAsync(int userId, FilterCustomerOperationRequest filterRequest, int pageNumber, int pageSize);
        Task<List<DisplayCustomerOperationResponse>> TGetCancelledFilteredOperationsByUserIdAsync(int userId, FilterCustomerOperationRequest filterRequest, int pageNumber, int pageSize);
        Task<List<DisplayEmailInteractionCountResponse>> GetTopEmailInteractions();
        Task<List<DisplayFaceToFaceInteractionCountResponse>> GetTopFaceToFaceInteractions();
        Task<List<DisplayPhoneInteractionCountResponse>> GetTopPhoneInteractions();
        Task<List<DisplayUserEmailInteractionCountResponse>> GetUserEmailInteractions(int userId);
        Task<List<DisplayUserPhoneInteractionCountResponse>> GetUserPhoneInteractions(int userId);
        Task<List<DisplayUserFaceToFaceInteractionCountResponse>> GetUserFaceToFaceInteractions(int userId);
        Task<DisplayOperationStatsResponse> GetTotalOperationStatsAsync();
        Task<DisplayUserOperationStatsResponse> GetUserOperationStatsAsync(int userId);


    }
}
