﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCustomerOperationRepository : IRepository<CustomerOperation>, ICustomerOperationRepository
    {
        private readonly ApplicationDbContext _context;

        public EfCustomerOperationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(CustomerOperation entity)
        {
            await _context.Set<CustomerOperation>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(CustomerOperation entity)
        {
            _context.Set<CustomerOperation>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<CustomerOperation>> GetAll()
        {
            return await _context.CustomerOperations
            .Include(co => co.User)
            .Include(co => co.Customer)
            .ToListAsync();
        }

        public async Task<List<CustomerOperation>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.CustomerOperations
                .Include(co => co.User)
                .Include(co => co.Customer)
                .OrderByDescending(co => co.OperationDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<CustomerOperation> GetById(int id)
        {
            return await _context.Set<CustomerOperation>().FindAsync(id);
        }

        public async Task<List<CustomerOperation>> GetOperationsByCustomerId(int customerId)
        {
            return await _context.CustomerOperations
       .Include(co => co.User)
       .Include(co => co.Customer)
       .OrderByDescending(co => co.OperationDate)
       .Where(co => co.CustomerId == customerId)
       .ToListAsync();
        }

        public async Task<(List<CustomerOperation>, int)> GetOperationsByUserId(int userId, int pageNumber, int pageSize)
        {
            var query = _context.CustomerOperations
                .Include(co => co.User)
                .Include(co => co.Customer)
                .Where(co => co.UserId == userId)
                .OrderByDescending(co => co.OperationDate);

            int totalRecords = await query.CountAsync();

            var operations = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (operations, totalRecords);
        }

        public async Task<List<CustomerOperation>> GetPagedCustomerOperationsAsync(int pageNumber, int pageSize)
        {
            return await _context.CustomerOperations
       .Include(co => co.User)
       .Include(co => co.Customer)
       .OrderByDescending(co => co.OperationDate)
       .Skip((pageNumber - 1) * pageSize)
       .Take(pageSize)
       .ToListAsync();
        }

        public async Task Update(CustomerOperation entity)
        {
            _context.Set<CustomerOperation>().Update(entity);
            _context.SaveChanges();
        }
    }
}
