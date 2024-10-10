using DtoLayer.TaskAssignment.Requests;
using DtoLayer.TaskAssignment.Responses;
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
        Task<GetByIdTaskAssignmentResponse> GetUserByIdAsync(int id);
        Task AddTaskAssignmentAsync(AddTaskAssignmentRequest addTaskAssignmentRequest);
        Task UpdateTaskAssignmentAsync(int id,UpdateTaskAssignmentRequest updateTaskAssignmentRequest);
        Task DeleteTaskAssignmentAsync(int id);
    }
}
