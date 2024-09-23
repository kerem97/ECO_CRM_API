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

        public CustomerOperationService(ICustomerOperationRepository customerOperationRepository, IMapper mapper)
        {
            _customerOperationRepository = customerOperationRepository;
            _mapper = mapper;
        }

        public async Task AddCustomerOperationsAsync(AddCustomerOperationRequest addCustomerOperationRequest)
        {
            var customerOperationEntity = _mapper.Map<CustomerOperation>(addCustomerOperationRequest);
            await _customerOperationRepository.Add(customerOperationEntity);
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

        public async Task<GetByIdCustomerOperationResponse> GetCustomerOperationByIdAsync(int id)
        {
            var operation = await _customerOperationRepository.GetById(id);
            return _mapper.Map<GetByIdCustomerOperationResponse>(operation);
        }

        public async Task UpdateCustomerOperationsAsync(UpdateCustomerOperationRequest updateCustomerOperationRequest)
        {
            var operationEntity = await _customerOperationRepository.GetById(updateCustomerOperationRequest.Id);
            if (operationEntity != null)
            {
                _mapper.Map(updateCustomerOperationRequest, operationEntity);
                _customerOperationRepository.Update(operationEntity);
            }
        }
    }
}
