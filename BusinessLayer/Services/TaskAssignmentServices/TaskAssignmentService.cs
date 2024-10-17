using AutoMapper;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
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
        private readonly ICustomerOperationRepository _customerOperationRepository;
        private readonly IMapper _mapper;

        public TaskAssignmentService(ITaskAssignmentRepository repository, IMapper mapper, ICustomerOperationRepository customerOperationRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _customerOperationRepository = customerOperationRepository;
        }

        public async Task AddTaskAssignmentAsync(AddTaskAssignmentRequest request)
        {
            var operation = await _customerOperationRepository.GetById(request.OperationId);
            if (operation == null)
            {
                throw new Exception("Operation not found");
            }
            operation.OfferStatus = "Fiyat Talep Edildi";
            await _customerOperationRepository.Update(operation);
            var taskAssignment = new TaskAssignment
            {
                OperationId = request.OperationId,
                CustomerId = operation.CustomerId,
                UserId = operation.UserId,
                AbasId = request.AbasId,
                Description = request.Description,
                Status = "Fiyat Bekleniyor",
                Quantity1 = request.Quantity1,
                Quantity2 = request.Quantity2,
                Quantity3 = request.Quantity3,
                Quantity4 = request.Quantity4,
                Quantity5 = request.Quantity5,
                Quantity6 = request.Quantity6,
                Quantity7 = request.Quantity7,
                Quantity8 = request.Quantity8,
                Quantity9 = request.Quantity9,
                Quantity10 = request.Quantity10,
                CreatedDate = DateTime.Now
            };

            await _repository.Add(taskAssignment);
        }

        public async Task UpdateTaskStatusToOfferGivenAsync(UpdateTaskAssignmentStatusToOfferGivenRequest request)
        {
            var task = await _repository.GetById(request.Id);
            if (task == null)
            {
                throw new Exception("Görev bulunamadı.");
            }

            task.Status = "Fiyat Verildi";
            await _repository.Update(task);
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

        public async Task<GetByIdTaskAssignmentResponse> GetTaskAssignmentByIdAsync(int id)
        {
            var taskAssignment = await _repository.GetById(id);
            if (taskAssignment == null)
            {
                throw new Exception("Görev bulunamadı.");
            }

            var taskAssignmentResponse = _mapper.Map<GetByIdTaskAssignmentResponse>(taskAssignment);

            return taskAssignmentResponse;
        }

        public async Task<List<GetPendingTaskAssignmentResponse>> TGetPendingTasksAsync(int pageNumber, int pageSize)
        {
            var pendingTasks = await _repository.GetPendingTasksAsync(pageNumber, pageSize);

            var result = _mapper.Map<List<GetPendingTaskAssignmentResponse>>(pendingTasks);

            return result;
        }
        public async Task<List<GetComplatedTaskAssignmentResponse>> TGetCompletedTasksAsync(int pageNumber, int pageSize)
        {
            var complatedTasks = await _repository.GetCompletedTasksAsync(pageNumber, pageSize);

            var result = _mapper.Map<List<GetComplatedTaskAssignmentResponse>>(complatedTasks);

            return result;
        }
        public async Task UpdateTaskAssignmentAsync(int id, UpdateTaskAssignmentRequest updateTaskAssignmentRequest)
        {
            var existingTaskAssignment = await _repository.GetById(id);
            if (existingTaskAssignment == null)
            {
                throw new Exception("Güncellenecek görev bulunamadı.");
            }

            _mapper.Map(updateTaskAssignmentRequest, existingTaskAssignment);

            await _repository.Update(existingTaskAssignment);
        }

        public async Task UpdateTaskStatusToProposalGivenAsync(UpdateTaskStatusToProposalGivenRequest request)
        {
            var task = await _repository.GetById(request.Id);
            if (task == null)
            {
                throw new Exception("Görev bulunamadı.");
            }

            task.Status = "Teklif Verildi";
            await _repository.Update(task);
        }

        public async Task<List<GetProposalGivenTaskAssignmentResponse>> TGetProposalGivenTasksAsync(int pageNumber, int pageSize)
        {
            var pendingTasks = await _repository.GetProposalGivenTasksAsync(pageNumber, pageSize);

            var result = _mapper.Map<List<GetProposalGivenTaskAssignmentResponse>>(pendingTasks);

            return result;
        }

        public async Task UpdateTaskStatusToApprovedAsync(UpdateTaskStatusToApprovedRequest request)
        {
            var task = await _repository.GetById(request.Id);
            if (task == null)
            {
                throw new Exception("Görev bulunamadı.");
            }

            task.Status = "Onaylandı";
            task.Description = request.Description;
            task.CompletedDate = DateTime.Now;
            await _repository.Update(task);
        }

        public async Task UpdateTaskStatusToRejectedAsync(UpdateTaskStatusToRejectedRequest request)
        {
            var task = await _repository.GetById(request.Id);
            if (task == null)
            {
                throw new Exception("Görev bulunamadı.");
            }

            task.Status = "Onaylanmadı";
            task.Description = request.Description; 
            task.CompletedDate= DateTime.Now;
            await _repository.Update(task);
        }

        public async Task<TaskAssignmentApprovedCountResponse> TGetApprovedTaskCountByCustomerIdAsync(int customerId)
        {
            var approvedTaskCount = await _repository.GetApprovedTaskCountByCustomerIdAsync(customerId);

            return new TaskAssignmentApprovedCountResponse
            {
                ApprovedTaskCount = approvedTaskCount
            };
        }

        public async Task<TaskAssignmentNotApprovedCountResponse> TGetNotApprovedTaskCountByCustomerIdAsync(int customerId)
        {
            var notApprovedTaskCount = await _repository.GetNotApprovedTaskCountByCustomerIdAsync(customerId);

            return new TaskAssignmentNotApprovedCountResponse
            {
                NotApprovedTaskCount = notApprovedTaskCount
            };
        }

        public async Task<GetTotalTaskAssignmentCountByCustomerIdResponse> GetTaskAssignmentCountAsync(int customerId)
        {
            var count = await _repository.GetTaskAssignmentCountByCustomerIdAsync(customerId);
            return new GetTotalTaskAssignmentCountByCustomerIdResponse { TaskCount = count };
        }

        public async Task<List<GetLast10TaskAssignmentsByCustomerIdResponse>> TGetLast10TaskAssignmentsByCustomerIdAsync(int customerId)
        {
            var taskAssignments = await _repository.GetLast10TaskAssignmentsByCustomerIdAsync(customerId);

            // DTO'ya map'le
            var taskAssignmentDtos = _mapper.Map<List<GetLast10TaskAssignmentsByCustomerIdResponse>>(taskAssignments);

            return taskAssignmentDtos;
        }
    }
}
