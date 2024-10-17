using DtoLayer.TaskAssignment.Requests;
using DtoLayer.TaskAssignment.Responses;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.TaskAssignmentServices
{
    public interface ITaskAssignmentService
    {
        Task<List<DisplayTaskAssignmentResponse>> GetAllTaskAssignmentAsync();
        Task<GetByIdTaskAssignmentResponse> GetTaskAssignmentByIdAsync(int id);
        Task AddTaskAssignmentAsync(AddTaskAssignmentRequest addTaskAssignmentRequest);
        Task UpdateTaskAssignmentAsync(int id, UpdateTaskAssignmentRequest updateTaskAssignmentRequest);
        Task DeleteTaskAssignmentAsync(int id);
        Task UpdateTaskStatusToOfferGivenAsync(UpdateTaskAssignmentStatusToOfferGivenRequest request);
        Task UpdateTaskStatusToProposalGivenAsync(UpdateTaskStatusToProposalGivenRequest request);
        Task UpdateTaskStatusToApprovedAsync(UpdateTaskStatusToApprovedRequest request);
        Task UpdateTaskStatusToRejectedAsync(UpdateTaskStatusToRejectedRequest request);
        Task<List<GetPendingTaskAssignmentResponse>> TGetPendingTasksAsync(int pageNumber, int pageSize);
        Task<List<GetProposalGivenTaskAssignmentResponse>> TGetProposalGivenTasksAsync(int pageNumber, int pageSize);
        Task<List<GetComplatedTaskAssignmentResponse>> TGetCompletedTasksAsync(int pageNumber, int pageSize);
        Task<TaskAssignmentApprovedCountResponse> TGetApprovedTaskCountByCustomerIdAsync(int customerId);
        Task<TaskAssignmentNotApprovedCountResponse> TGetNotApprovedTaskCountByCustomerIdAsync(int customerId);
        Task<GetTotalTaskAssignmentCountByCustomerIdResponse> GetTaskAssignmentCountAsync(int customerId);
        Task<List<GetLast10TaskAssignmentsByCustomerIdResponse>> TGetLast10TaskAssignmentsByCustomerIdAsync(int customerId);
    }
}
