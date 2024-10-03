using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<List<CustomerOperation>> GetFilteredOperationsAsync(string companyName, int? month, int? year, string method, string performedBy, string reason, string status, int pageNumber, int pageSize)
        {
            var query = _context.CustomerOperations.Include(co => co.Customer).Include(co => co.User).AsQueryable();

            if (!string.IsNullOrEmpty(companyName))
                query = query.Where(co => co.Customer.CompanyName == companyName);

            if (month.HasValue)
                query = query.Where(co => co.PlannedDate.HasValue && co.PlannedDate.Value.Month == month.Value);

            if (year.HasValue)
                query = query.Where(co => co.PlannedDate.HasValue && co.PlannedDate.Value.Year == year.Value);

            if (!string.IsNullOrEmpty(method))
                query = query.Where(co => co.Method == method);

            if (!string.IsNullOrEmpty(performedBy))
                query = query.Where(co => co.User.FullName == performedBy);

            if (!string.IsNullOrEmpty(reason))
                query = query.Where(co => co.Reason == reason);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(co => co.Status == status);

            return await query
        .OrderByDescending(co => co.OperationDate)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
        }
        public async Task<List<CustomerOperation>> GetFilteredOperationsByUserIdAsync(int userId, string companyName, int? month, int? year, string method, string performedBy, string reason, string status, int pageNumber, int pageSize)
        {
            var query = _context.CustomerOperations
                .Include(co => co.Customer)
                .Include(co => co.User)
                .Where(co => co.UserId == userId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(companyName))
                query = query.Where(co => co.Customer.CompanyName == companyName);

            if (month.HasValue)
                query = query.Where(co => co.PlannedDate.HasValue && co.PlannedDate.Value.Month == month.Value);

            if (year.HasValue)
                query = query.Where(co => co.PlannedDate.HasValue && co.PlannedDate.Value.Year == year.Value);

            if (!string.IsNullOrEmpty(method))
                query = query.Where(co => co.Method == method);

            if (!string.IsNullOrEmpty(performedBy))
                query = query.Where(co => co.User.FullName == performedBy);

            if (!string.IsNullOrEmpty(reason))
                query = query.Where(co => co.Reason == reason);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(co => co.Status == status);

            return await query
                .OrderByDescending(co => co.OperationDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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

        public async Task<List<(string companyName, int count)>> GetTopEmailInteractions()
        {
            var result = _context.CustomerOperations
                .Where(co => co.Method == "E-Mail")
                .GroupBy(co => co.Customer.CompanyName)
                .Select(g => new { CompanyName = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .ToList()
                .Select(x => (x.CompanyName, x.Count))
                .ToList();

            return result;


        }



        public async Task<List<(string companyName, int count)>> GetTopFaceToFaceInteractions()
        {
            var result = _context.CustomerOperations
                .Where(co => co.Method == "Yüz Yüze")
                .GroupBy(co => co.Customer.CompanyName)
                .Select(g => new { CompanyName = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .ToList()
                .Select(x => (x.CompanyName, x.Count))
                .ToList();

            return result;
        }

        public async Task<List<(string companyName, int count)>> GetTopPhoneInteractions()
        {
            var result = _context.CustomerOperations
             .Where(co => co.Method == "Telefon")
             .GroupBy(co => co.Customer.CompanyName)
             .Select(g => new { CompanyName = g.Key, Count = g.Count() })
             .OrderByDescending(g => g.Count)
             .Take(5)
             .ToList()
             .Select(x => (x.CompanyName, x.Count))
             .ToList();

            return result;
        }
        public async Task<List<(string companyName, int customerId, int count)>> GetUserEmailInteractions(int userId)
        {
            var result = await _context.CustomerOperations
                .Where(co => co.Method == "E-Mail" && co.UserId == userId)
                .GroupBy(co => new { co.Customer.CompanyName, co.Customer.Id })
                .Select(g => new { g.Key.CompanyName, g.Key.Id, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .ToListAsync();

            return result.Select(x => (x.CompanyName, x.Id, x.Count)).ToList();
        }
        public async Task<List<(string companyName, int customerId, int count)>> GetUserFaceToFaceInteractions(int userId)
        {
            var result = await _context.CustomerOperations
       .Where(co => co.Method == "Yüz Yüze" && co.UserId == userId)
       .Include(co => co.Customer)
       .GroupBy(co => new { co.Customer.CompanyName, co.Customer.Id })
       .Select(g => new { g.Key.CompanyName, g.Key.Id, Count = g.Count() })
       .OrderByDescending(g => g.Count)
       .Take(5)
       .ToListAsync();

            return result.Select(x => (x.CompanyName, x.Id, x.Count)).ToList();
        }
        public async Task<List<(string companyName, int customerId, int count)>> GetUserPhoneInteractions(int userId)
        {
            var result = await _context.CustomerOperations
         .Where(co => co.Method == "Telefon" && co.UserId == userId)
         .Include(co => co.Customer)
         .GroupBy(co => new { co.Customer.CompanyName, co.Customer.Id })
         .Select(g => new { g.Key.CompanyName, g.Key.Id, Count = g.Count() })
         .OrderByDescending(g => g.Count)
         .Take(5)
         .ToListAsync();

            return result.Select(x => (x.CompanyName, x.Id, x.Count)).ToList();
        }
        public async Task<(int plannedCount, int completedCount)> GetTotalOperationStatsAsync()
        {
            var plannedCount = await _context.CustomerOperations.CountAsync(co => co.Status == "Planlandı");
            var completedCount = await _context.CustomerOperations.CountAsync(co => co.Status == "Gerçekleşti");


            return (plannedCount, completedCount);
        }
        public async Task<(int plannedCount, int completedCount)> GetUserOperationStatsAsync(int userId)
        {
            var plannedCount = await _context.CustomerOperations.CountAsync(co => co.UserId == userId && co.Status == "Planlandı");
            var completedCount = await _context.CustomerOperations.CountAsync(co => co.UserId == userId && co.Status == "Gerçekleşti");

            return (plannedCount, completedCount);
        }
        public async Task Update(CustomerOperation entity)
        {
            _context.Set<CustomerOperation>().Update(entity);
            _context.SaveChanges();
        }
    }
}
