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

        public async void Delete(CustomerOperation entity)
        {
            _context.Set<CustomerOperation>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<CustomerOperation>> GetAll()
        {
            return await _context.Set<CustomerOperation>().ToListAsync();
        }

        public async Task<CustomerOperation> GetById(int id)
        {
            return await _context.Set<CustomerOperation>().FindAsync(id);
        }

        public async void Update(CustomerOperation entity)
        {   
            _context.Set<CustomerOperation>().Update(entity);
            _context.SaveChanges();
        }
    }
}
