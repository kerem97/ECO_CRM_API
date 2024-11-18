using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfTaskAssignmentFileRepository : ITaskAssignmentFileRepository
    {
        private readonly ApplicationDbContext _context;

        public EfTaskAssignmentFileRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(TaskAssignmentFile entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            await _context.Set<TaskAssignmentFile>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var exist = await _context.Customers.FindAsync(id);
            if (exist != null)
            {
                _context.Customers.Remove(exist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TaskAssignmentFile>> GetAll()
        {
            return await _context.Set<TaskAssignmentFile>().ToListAsync();
        }

        public async Task<TaskAssignmentFile> GetById(int id)
        {
            return await _context.Set<TaskAssignmentFile>().FindAsync(id);
        }

        public async Task Update(TaskAssignmentFile entity)
        {
            _context.Set<TaskAssignmentFile>().Update(entity);
            _context.SaveChanges();
        }
    }
}
