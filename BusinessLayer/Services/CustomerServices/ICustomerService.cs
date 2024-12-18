﻿using DtoLayer.Customer.Requests;
using DtoLayer.Customer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<List<DisplayCustomerResponse>> GetAllCustomersAsync();
        Task<GetByIdCustomerResponse> GetCustomerByIdAsync(int id);
        Task AddCustomersAsync(AddCustomerRequest addCustomerRequest, int createdByUserId);
        Task AddPotentialCustomersAsync(AddPotentialCustomerRequest addPotentialCustomerRequest, int createdByUserId);
        Task UpdateCustomersAsync(UpdateCustomerRequest updateCustomerRequest, int updatedByUserId);
        Task DeleteCustomersAsync(int id);
        Task<(List<DisplayCustomerResponse>, int)> GetPagedCustomersAsync(int pageNumber, int pageSize);
        Task<(List<DisplayCustomerResponse>, int)> TGetAllExistedCustomersPaged(int pageNumber, int pageSize);
        Task<(List<DisplayCustomerResponse>, int)> TGetAllPotentialCustomersPaged(int pageNumber, int pageSize);
        Task<List<SearchCustomerDto>> SearchCompaniesByNameAddOperations(string searchTerm, int pageNumber, int pageSize);
        Task<List<string>> SearchCompaniesByName(string searchTerm, int pageNumber, int pageSize);
        Task<GetProfileInfoByIdResponse> GetProfileInfoByIdAsync(int id);
        Task<GetCustomerCreatorResponse> GetCustomerCreatorByCustomerIdAsync(int customerId);
    }
}
