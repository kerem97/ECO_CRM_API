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
    public class EfCustomerOperationFileRepository : ICustomerOperationFileRepository
    {
        private readonly ApplicationDbContext _context;

        public EfCustomerOperationFileRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(CustomerOperationFile entity)
        {
            await _context.CustomerOperationFiles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<CustomerOperationFile>> GetFilesByOperationIdAsync(int operationId)
        {
            return await _context.CustomerOperationFiles
                .Where(f => f.CustomerOperationId == operationId)
                .ToListAsync();
        }

        public async Task Delete(int id)
        {
            var file = await _context.CustomerOperationFiles.FindAsync(id);
            if (file != null)
            {
                _context.CustomerOperationFiles.Remove(file);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CustomerOperationFile>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerOperationFile> GetById(int id)
        {
            return await _context.CustomerOperationFiles.FindAsync(id);
        }

        public async Task Update(CustomerOperationFile entity)
        {
            throw new NotImplementedException();
        }
    }
}
