using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DtoLayer.TaskAssignment.Responses;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfTaskAssignmentRepository : ITaskAssignmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EfTaskAssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(TaskAssignment entity)
        {
            await _context.Set<TaskAssignment>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TaskAssignment entity)
        {
            _context.Set<TaskAssignment>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<TaskAssignment>> GetAll()
        {
            return await _context.Set<TaskAssignment>().ToListAsync();
        }

        public async Task<TaskAssignment> GetById(int id)
        {
          return await _context.TaskAssignments
        .Include(ta => ta.CustomerOperation)
        .ThenInclude(co => co.Customer)
        .Include(ta => ta.CustomerOperation.User)
         .FirstOrDefaultAsync(ta => ta.Id == id);
        }

        public async Task<List<TaskAssignmentEfDto>> GetPendingTasksAsync(int pageNumber, int pageSize)
        {
            return await _context.TaskAssignments
        .Include(ta => ta.CustomerOperation) 
        .ThenInclude(co => co.Customer)
        .Include(ta => ta.CustomerOperation.User)
        .Where(ta => ta.Status == "Fiyat Bekleniyor")
        .OrderByDescending(ta => ta.CreatedDate)  
        .Skip((pageNumber - 1) * pageSize)  
        .Take(pageSize)
        .Select(ta => new TaskAssignmentEfDto
        {
            Id = ta.Id,
            CreatedDate = ta.CreatedDate,
            CustomerName = ta.CustomerOperation.Customer.CompanyName,
            CreatedByUser = ta.CustomerOperation.User.FullName,
            AbasId = ta.AbasId,
            Description = ta.Description,
            Status = ta.Status,
            Quantity1 = ta.Quantity1,
            Quantity2 = ta.Quantity2,
            Quantity3 = ta.Quantity3,
            Quantity4 = ta.Quantity4,
            Quantity5 = ta.Quantity5,
            Quantity6 = ta.Quantity6,
            Quantity7 = ta.Quantity7,
            Quantity8 = ta.Quantity8,
            Quantity9 = ta.Quantity9,
            Quantity10 = ta.Quantity10
        })
        .ToListAsync();

        }

        public async Task Update(TaskAssignment entity)
        {
            _context.Set<TaskAssignment>().Update(entity);
            _context.SaveChanges();
        }
    }
}
