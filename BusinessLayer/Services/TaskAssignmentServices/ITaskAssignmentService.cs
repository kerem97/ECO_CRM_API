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
        Task UpdateTaskStatusAsync(UpdateTaskAssignmentStatusToOfferGivenRequest request);
        Task<List<GetPendingTaskAssignmentResponse>> TGetPendingTasksAsync(int pageNumber, int pageSize);
    }
}
