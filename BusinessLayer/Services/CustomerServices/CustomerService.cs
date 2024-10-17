using AutoMapper;
using DataAccessLayer.Abstract;
using DtoLayer.Customer.Requests;
using DtoLayer.Customer.Responses;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task AddCustomersAsync(AddCustomerRequest addCustomerRequest, int createdByUserId)
        {
            addCustomerRequest.CreatedDate = DateTime.Now;
            var customerEntity = _mapper.Map<Customer>(addCustomerRequest);
            customerEntity.UserId = createdByUserId;

            await _customerRepository.Add(customerEntity);
        }
        public async Task DeleteCustomersAsync(int id)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer != null)
            {
                _customerRepository.Delete(customer);
            }
        }
        public async Task<List<DisplayCustomerResponse>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAll();

            var customerResponses = customers.Select(customer => new DisplayCustomerResponse
            {
                Id = customer.Id,
                CreatedByUser = customer.User != null ? customer.User.FullName : "Unknown",
                CompanyName = customer.CompanyName,
                Address = customer.Address,
                District = customer.District,
                City = customer.City,
                Country = customer.Country,
                ContactName = customer.ContactName,
                ContactPhone1 = customer.ContactPhone1,
                ContactPhone2 = customer.ContactPhone2,
                ContactEmail = customer.ContactEmail,
                IsDomestic = customer.IsDomestic,
                Description = customer.Description,
                Status = customer.Status,
                LimitTl = customer.LimitTl
            }).ToList();

            return customerResponses;
        }
        public async Task<GetByIdCustomerResponse> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
                return null;

            return new GetByIdCustomerResponse
            {
                Id = customer.Id,
                CreatedByUser = customer.User != null ? customer.User.FullName : "Unknown",
                CompanyName = customer.CompanyName,
                Address = customer.Address,
                District = customer.District,
                City = customer.City,
                Country = customer.Country,
                ContactName = customer.ContactName,
                ContactPhone1 = customer.ContactPhone1,
                ContactPhone2 = customer.ContactPhone2,
                ContactEmail = customer.ContactEmail,
                IsDomestic = customer.IsDomestic,
                Description = customer.Description,
                Status = customer.Status,
                LimitTl = customer.LimitTl
            };
        }
        public async Task<(List<DisplayCustomerResponse>, int)> GetPagedCustomersAsync(int pageNumber, int pageSize)
        {
            var (customers, totalCustomers) = await _customerRepository.GetAllPaged(pageNumber, pageSize);

            var customerResponses = _mapper.Map<List<DisplayCustomerResponse>>(customers);

            return (customerResponses, totalCustomers);
        }
        public async Task<(List<DisplayCustomerResponse>, int)> TGetAllExistedCustomersPaged(int pageNumber, int pageSize)
        {
            var (customers, totalCustomers) = await _customerRepository.GetAllExistedCustomersPaged(pageNumber, pageSize);

            var customerResponses = _mapper.Map<List<DisplayCustomerResponse>>(customers);

            return (customerResponses, totalCustomers);
        }
        public async Task<(List<DisplayCustomerResponse>, int)> TGetAllPotentialCustomersPaged(int pageNumber, int pageSize)
        {
            var (customers, totalCustomers) = await _customerRepository.GetAllPotentialCustomersPaged(pageNumber, pageSize);

            var customerResponses = _mapper.Map<List<DisplayCustomerResponse>>(customers);

            return (customerResponses, totalCustomers);
        }
        public async Task<List<SearchCustomerDto>> SearchCompaniesByNameAddOperations(string searchTerm, int pageNumber, int pageSize)
        {
            return await _customerRepository.SearchCompaniesByNameAddOperations(searchTerm, pageNumber, pageSize);
        }
        public async Task<List<string>> SearchCompaniesByName(string searchTerm, int pageNumber, int pageSize)
        {
            return await _customerRepository.SearchCompaniesByName(searchTerm, pageNumber, pageSize);
        }
        public async Task UpdateCustomersAsync(UpdateCustomerRequest updateCustomerRequest, int updatedByUserId)
        {
            var customerEntity = await _customerRepository.GetById(updateCustomerRequest.Id);
            if (customerEntity != null)
            {
                _mapper.Map(updateCustomerRequest, customerEntity);
                customerEntity.UserId = updatedByUserId;

                _customerRepository.Update(customerEntity);
            }
        }

        public async Task<GetProfileInfoByIdResponse> GetProfileInfoByIdAsync(int id)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null) throw new Exception("Customer not found.");
            var customerDto = new GetProfileInfoByIdResponse
            {
                CompanyName = customer.CompanyName,
                Website = customer.Website,
                Instagram = customer.Instagram,
                Twitter = customer.Twitter,
                Facebook = customer.Facebook,
                Status = customer.Status,
                ContactPhone1 = customer.ContactPhone1,
                ContactEmail = customer.ContactEmail
            };

            return customerDto;
        }

        public async Task<GetCustomerCreatorResponse> GetCustomerCreatorByCustomerIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerWithUserByIdAsync(customerId);  
            if (customer == null)
            {
                return null; 
            }

            var customerCreatorDto = _mapper.Map<GetCustomerCreatorResponse>(customer);
            return customerCreatorDto;
        }
    }
}
