using AutoMapper;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
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
    public class CustomerOperationService : ICustomerOperationService
    {
        private readonly ICustomerOperationRepository _customerOperationRepository;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        public CustomerOperationService(ICustomerOperationRepository customerOperationRepository, IMapper mapper, ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            _customerOperationRepository = customerOperationRepository;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }
        public async Task AddCustomerOperationsAsync(AddCustomerOperationRequest addCustomerOperationRequest, int userId)
        {
            var customerOperation = _mapper.Map<CustomerOperation>(addCustomerOperationRequest);
            customerOperation.UserId = userId;
            customerOperation.OperationDate = DateTime.Now;

            var userExists = await _userRepository.GetById(userId);
            var customerExists = await _customerRepository.GetById(addCustomerOperationRequest.CustomerId);

            if (userExists == null)
            {
                throw new Exception("User not found");
            }

            if (customerExists == null)
            {
                throw new Exception("Customer not found");
            }
            if (addCustomerOperationRequest.ActualDate == null)
            {
                customerOperation.Status = "Planlandı";
            }
            else
            {
                customerOperation.Status = "Tamamlandı";
            }

            await _customerOperationRepository.Add(customerOperation);
        }
        public async Task CancelOperationAsync(int operationId, string cancelReason)
        {
            var operation = await _customerOperationRepository.GetById(operationId);
            if (operation == null)
                throw new Exception("Operation not found");

            operation.Status = "İptal Edildi";
            operation.CancelReason = cancelReason;

            await _customerOperationRepository.Update(operation);
        }
        public async Task CompleteOperationAsync(int operationId, DateTime actualDate, bool? isMeetingOnPlannedDate, string updatedStatusDescription, string offerStatus)
        {
            var operation = await _customerOperationRepository.GetById(operationId);
            if (operation == null)
                throw new Exception("Operation not found");

            if (operation.Status != "Planlandı")
                throw new Exception("Sadece planlanan işlemler güncellenebilir!");

            operation.Status = "Gerçekleşti";
            if (isMeetingOnPlannedDate == true)
            {
                operation.ActualDate = operation.PlannedDate;
                operation.IsMeetingOnPlannedDate = true;
            }
            else if (isMeetingOnPlannedDate == false && actualDate != null)
            {
                operation.ActualDate = actualDate;
                operation.IsMeetingOnPlannedDate = false;
            }

            operation.UpdatedStatusDescription = updatedStatusDescription;
            operation.OfferStatus = offerStatus;

            await _customerOperationRepository.Update(operation);
        }
        public async Task DeleteCustomerOperationsAsync(int id)
        {
            var operation = await _customerOperationRepository.GetById(id);
            if (operation != null)
            {
                _customerOperationRepository.Delete(operation);
            }
        }
        public async Task<List<DisplayCustomerOperationResponse>> GetAllCustomerOperationsAsync()
        {
            var operations = await _customerOperationRepository.GetAll();
            return _mapper.Map<List<DisplayCustomerOperationResponse>>(operations);
        }
        public async Task<List<DisplayCustomerOperationResponse>> GetAllCustomerOperationsPagedAsync(int pageNumber, int pageSize)
        {
            var operations = await _customerOperationRepository.GetAllPagedAsync(pageNumber, pageSize);
            return _mapper.Map<List<DisplayCustomerOperationResponse>>(operations);
        }
        public async Task<GetByIdCustomerOperationResponse> GetCustomerOperationByIdAsync(int id)
        {
            var operation = await _customerOperationRepository.GetById(id);
            return _mapper.Map<GetByIdCustomerOperationResponse>(operation);
        }
        public async Task<List<DisplayCustomerOperationByCustomerResponse>> GetOperationsByCustomerIdAsync(int customerId)
        {
            var operations = await _customerOperationRepository.GetOperationsByCustomerId(customerId);
            return _mapper.Map<List<DisplayCustomerOperationByCustomerResponse>>(operations);
        }
        public async Task<List<DisplayCustomerOperationResponse>> GetPagedCustomerOperationsAsync(int pageNumber, int pageSize)
        {
            var operations = await _customerOperationRepository.GetPagedCustomerOperationsAsync(pageNumber, pageSize);
            return _mapper.Map<List<DisplayCustomerOperationResponse>>(operations);
        }
        public async Task<(List<DisplayCustomerOperationResponse>, int)> GetUserPlannedOperationsAsync(int userId, int pageNumber, int pageSize)
        {
            var (operations, totalOperations) = await _customerOperationRepository.GetPlannedOperationsByUserId(userId, pageNumber, pageSize);

            var mappedOperations = _mapper.Map<List<DisplayCustomerOperationResponse>>(operations);

            return (mappedOperations, totalOperations);
        }
        public async Task UpdateCustomerOperationsAsync(UpdateCustomerOperationRequest updateCustomerOperationRequest, int userId)
        {
            var existingOperation = await _customerOperationRepository.GetById(updateCustomerOperationRequest.Id);

            if (existingOperation == null)
            {
                throw new Exception("Customer operation not found");
            }

            var userExists = await _userRepository.GetById(userId);
            if (userExists == null)
            {
                throw new Exception("User not found");
            }

            var customerExists = await _customerRepository.GetById(updateCustomerOperationRequest.CustomerId);
            if (customerExists == null)
            {
                throw new Exception("Customer not found");
            }

            _mapper.Map(updateCustomerOperationRequest, existingOperation);
            existingOperation.UserId = userId;

            _customerOperationRepository.Update(existingOperation);
        }
        public async Task<List<DisplayCustomerOperationResponse>> GetFilteredOperationsAsync(FilterCustomerOperationRequest filterRequest, int pageNumber, int pageSize)
        {

            var filteredOperations = await _customerOperationRepository.GetFilteredOperationsAsync(
                filterRequest.CompanyName,
                filterRequest.Month,
                filterRequest.Year,
                filterRequest.Method,
                filterRequest.PerformedBy,
                filterRequest.Reason,
                filterRequest.Status,
                 pageNumber,
                 pageSize
            );

            return filteredOperations.Select(co => new DisplayCustomerOperationResponse
            {
                Id = co.Id,
                CustomerName = co.Customer.CompanyName,
                CreatedByUser = co.User.FullName,
                PlannedDate = co.PlannedDate,
                ActualDate = co.ActualDate,
                Status = co.Status,
                Method = co.Method,
                Reason = co.Reason,
                Address = co.Address,
                ContactPerson = co.ContactPerson,
                OperationDate = co.OperationDate,
                Description = co.Description
            }).ToList();
        }
        public async Task<List<DisplayCustomerOperationResponse>> GetFilteredOperationsByUserIdAsync(int userId, FilterCustomerOperationRequest filterRequest, int pageNumber, int pageSize)
        {
            var filteredOperations = await _customerOperationRepository.GetFilteredOperationsByUserIdAsync(
                userId,
                filterRequest.CompanyName,
                filterRequest.Month,
                filterRequest.Year,
                filterRequest.Method,
                filterRequest.PerformedBy,
                filterRequest.Reason,
                filterRequest.Status,
                pageNumber,
                pageSize
            );

            return _mapper.Map<List<DisplayCustomerOperationResponse>>(filteredOperations);
        }
        public async Task<List<DisplayEmailInteractionCountResponse>> GetTopEmailInteractions()
        {
            var data = await _customerOperationRepository.GetTopEmailInteractions();
            return data.Select(d => new DisplayEmailInteractionCountResponse
            {
                CompanyName = d.companyName,
                InteractionCount = d.count
            }).ToList();
        }
        public async Task<List<DisplayFaceToFaceInteractionCountResponse>> GetTopFaceToFaceInteractions()
        {
            var data = await _customerOperationRepository.GetTopFaceToFaceInteractions();
            return data.Select(d => new DisplayFaceToFaceInteractionCountResponse
            {
                CompanyName = d.companyName,
                InteractionCount = d.count,
            }).ToList();
        }
        public async Task<List<DisplayPhoneInteractionCountResponse>> GetTopPhoneInteractions()
        {
            var data = await _customerOperationRepository.GetTopPhoneInteractions();
            return data.Select(d => new DisplayPhoneInteractionCountResponse
            {
                CompanyName = d.companyName,
                InteractionCount = d.count,
            }).ToList();
        }
        public async Task<List<DisplayUserEmailInteractionCountResponse>> GetUserEmailInteractions(int userId)
        {
            var data = await _customerOperationRepository.GetUserEmailInteractions(userId);
            return data.Select(d => new DisplayUserEmailInteractionCountResponse
            {
                CompanyName = d.companyName,
                InteractionCount = d.count,
                CustomerId = d.customerId

            }).ToList();
        }
        public async Task<List<DisplayUserPhoneInteractionCountResponse>> GetUserPhoneInteractions(int userId)
        {
            var data = await _customerOperationRepository.GetUserPhoneInteractions(userId);
            return data.Select(d => new DisplayUserPhoneInteractionCountResponse
            {
                CompanyName = d.companyName,
                InteractionCount = d.count,
                CustomerId = d.customerId
            }).ToList();
        }
        public async Task<List<DisplayUserFaceToFaceInteractionCountResponse>> GetUserFaceToFaceInteractions(int userId)
        {
            var data = await _customerOperationRepository.GetUserFaceToFaceInteractions(userId);
            return data.Select(d => new DisplayUserFaceToFaceInteractionCountResponse
            {
                CompanyName = d.companyName,
                InteractionCount = d.count,
                CustomerId = d.customerId
            }).ToList();
        }
        public async Task<DisplayOperationStatsResponse> GetTotalOperationStatsAsync()
        {
            var (plannedCount, completedCount) = await _customerOperationRepository.GetTotalOperationStatsAsync();

            if (plannedCount == 0 && completedCount == 0)
            {
                return new DisplayOperationStatsResponse();
            }

            return new DisplayOperationStatsResponse
            {
                PlannedCount = plannedCount,
                CompletedCount = completedCount
            };
        }
        public async Task<DisplayUserOperationStatsResponse> GetUserOperationStatsAsync(int userId)
        {
            var (plannedCount, completedCount) = await _customerOperationRepository.GetUserOperationStatsAsync(userId);

            if (plannedCount == 0 && completedCount == 0)
            {
                return new DisplayUserOperationStatsResponse();
            }

            return new DisplayUserOperationStatsResponse
            {
                PlannedCount = plannedCount,
                CompletedCount = completedCount
            };
        }
        public async Task<(List<DisplayCustomerOperationResponse>, int)> GetUserComplatedOperationsAsync(int userId, int pageNumber, int pageSize)
        {
            var (operations, totalOperations) = await _customerOperationRepository.GetComplatedOperationsByUserId(userId, pageNumber, pageSize);

            var mappedOperations = _mapper.Map<List<DisplayCustomerOperationResponse>>(operations);

            return (mappedOperations, totalOperations);
        }
        public async Task<(List<DisplayCustomerOperationResponse>, int)> GetUserCancelledOperationsAsync(int userId, int pageNumber, int pageSize)
        {
            var (operations, totalOperations) = await _customerOperationRepository.GetCanceledOperationsByUserId(userId, pageNumber, pageSize);

            var mappedOperations = _mapper.Map<List<DisplayCustomerOperationResponse>>(operations);

            return (mappedOperations, totalOperations);
        }
        public async Task<List<DisplayCustomerOperationResponse>> TGetPlannedFilteredOperationsByUserIdAsync(int userId, FilterCustomerOperationRequest filterRequest, int pageNumber, int pageSize)
        {
            var filteredOperations = await _customerOperationRepository.GetPlannedFilteredOperationsByUserIdAsync(
                userId,
                filterRequest.CompanyName,
                filterRequest.Month,
                filterRequest.Year,
                filterRequest.Method,
                filterRequest.PerformedBy,
                filterRequest.Reason,
                filterRequest.Status,
                pageNumber,
                pageSize
            );

            return _mapper.Map<List<DisplayCustomerOperationResponse>>(filteredOperations);
        }
        public async Task<List<DisplayCustomerOperationResponse>> TGetComplatedFilteredOperationsByUserIdAsync(int userId, FilterCustomerOperationRequest filterRequest, int pageNumber, int pageSize)
        {
            var filteredOperations = await _customerOperationRepository.GetComplatedFilteredOperationsByUserIdAsync(
               userId,
               filterRequest.CompanyName,
               filterRequest.Month,
               filterRequest.Year,
               filterRequest.Method,
               filterRequest.PerformedBy,
               filterRequest.Reason,
               filterRequest.Status,
               pageNumber,
               pageSize
           );

            return _mapper.Map<List<DisplayCustomerOperationResponse>>(filteredOperations);
        }
        public async Task<List<DisplayCustomerOperationResponse>> TGetCancelledFilteredOperationsByUserIdAsync(int userId, FilterCustomerOperationRequest filterRequest, int pageNumber, int pageSize)
        {
            var filteredOperations = await _customerOperationRepository.GetCancelledFilteredOperationsByUserIdAsync(
                userId,
                filterRequest.CompanyName,
                filterRequest.Month,
                filterRequest.Year,
                filterRequest.Method,
                filterRequest.PerformedBy,
                filterRequest.Reason,
                filterRequest.Status,
                pageNumber,
                pageSize
            );

            return _mapper.Map<List<DisplayCustomerOperationResponse>>(filteredOperations);
        }
    }
}

