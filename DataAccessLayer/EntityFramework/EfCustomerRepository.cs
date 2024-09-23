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
    public class EfCustomerRepository : IRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public EfCustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Customer entity)
        {
            await _context.Set<Customer>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async void Delete(Customer entity)
        {
            _context.Set<Customer>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customers.Include(c => c.User).ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers
       .Include(c => c.User)
       .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async void Update(Customer entity)
        {
            _context.Set<Customer>().Update(entity);
            _context.SaveChanges();
        }
    }
}
