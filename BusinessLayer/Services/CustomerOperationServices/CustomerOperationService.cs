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

        public async Task<(List<DisplayCustomerOperationResponse>, int)> GetUserOperationsAsync(int userId, int pageNumber, int pageSize)
        {
            var (operations, totalOperations) = await _customerOperationRepository.GetOperationsByUserId(userId, pageNumber, pageSize);

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
    }
}

