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
    public class EfUserRepository :  IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public EfUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(User entity)
        {
            await _context.Set<User>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            _context.Set<User>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task Update(User entity)
        {
            _context.Set<User>().Update(entity);
            _context.SaveChanges();
        }
    }
}
