﻿using DataAccessLayer.Repository;
using DtoLayer.TaskAssignment.Responses;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITaskAssignmentRepository : IRepository<TaskAssignment>
    {
        Task<List<TaskAssignmentEfDto>> GetPendingTasksAsync(int pageNumber, int pageSize);
        Task<List<TaskAssignmentEfDto>> GetProposalGivenTasksAsync(int pageNumber, int pageSize);
        Task<List<TaskAssignmentEfDto>> GetCompletedTasksAsync(int pageNumber, int pageSize);
        Task<int> GetApprovedTaskCountByCustomerIdAsync(int customerId);
        Task<int> GetNotApprovedTaskCountByCustomerIdAsync(int customerId);
        Task<int> GetTaskAssignmentCountByCustomerIdAsync(int customerId);
        Task<List<TaskAssignment>> GetLast10TaskAssignmentsByCustomerIdAsync(int customerId);
        Task<TaskAssignment> GetByIdAsync(int id);
    }

}

