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

        public async Task Delete(Customer entity)
        {
            _context.Set<Customer>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customers.Include(c => c.User).ToListAsync();
        }

        public async Task<(List<Customer>, int)> GetAllPaged(int pageNumber, int pageSize)
        {
            var customersQuery = _context.Customers.Include(c => c.User)
                                                   .OrderBy(c => c.CompanyName);

            int totalCustomers = await customersQuery.CountAsync();

            var customers = await customersQuery.Skip((pageNumber - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();

            return (customers, totalCustomers);
        }


        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers
       .Include(c => c.User)
       .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(Customer entity)
        {
            _context.Set<Customer>().Update(entity);
            _context.SaveChanges();
        }
    }
}
