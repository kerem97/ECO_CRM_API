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
            return await _context.Set<TaskAssignment>().FindAsync(id);
        }

        public async Task Update(TaskAssignment entity)
        {
            _context.Set<TaskAssignment>().Update(entity);
            _context.SaveChanges();
        }
    }
}
