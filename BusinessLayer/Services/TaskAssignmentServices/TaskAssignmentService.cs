using AutoMapper;
using DataAccessLayer.Abstract;
using DtoLayer.TaskAssignment.Requests;
using DtoLayer.TaskAssignment.Responses;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.TaskAssignmentServices
{
    public class TaskAssignmentService : ITaskAssignmentService
    {
        private readonly ITaskAssignmentRepository _repository;
        private readonly IMapper _mapper;

        public TaskAssignmentService(ITaskAssignmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddTaskAssignmentAsync(AddTaskAssignmentRequest addTaskAssignmentRequest)
        {
            var taskAssignment = _mapper.Map<TaskAssignment>(addTaskAssignmentRequest);
            taskAssignment.CreatedDate = DateTime.Now;
            await _repository.Add(taskAssignment);
        }

        public async Task DeleteTaskAssignmentAsync(int id)
        {
            var taskAssignment = await _repository.GetById(id);
            if (taskAssignment == null)
            {
                throw new Exception("Görev bulunamadı.");
            }

            await _repository.Delete(taskAssignment);
        }

        public async Task<List<DisplayTaskAssignmentResponse>> GetAllTaskAssignmentAsync()
        {
            var taskAssignments = await _repository.GetAll();

            var taskAssignmentResponses = _mapper.Map<List<DisplayTaskAssignmentResponse>>(taskAssignments);

            return taskAssignmentResponses;
        }

        public async Task<GetByIdTaskAssignmentResponse> GetUserByIdAsync(int id)
        {
            var taskAssignment = await _repository.GetById(id);
            if (taskAssignment == null)
            {
                throw new Exception("Görev bulunamadı.");
            }

            var taskAssignmentResponse = _mapper.Map<GetByIdTaskAssignmentResponse>(taskAssignment);

            return taskAssignmentResponse;
        }

        public async Task UpdateTaskAssignmentAsync(int id,UpdateTaskAssignmentRequest updateTaskAssignmentRequest)
        {
            var existingTaskAssignment = await _repository.GetById(id); 
            if (existingTaskAssignment == null)
            {
                throw new Exception("Güncellenecek görev bulunamadı.");
            }

            _mapper.Map(updateTaskAssignmentRequest, existingTaskAssignment);

            await _repository.Update(existingTaskAssignment);
        }
    }
}
