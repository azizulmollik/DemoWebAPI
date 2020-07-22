using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoWebAPI.Data;
using DemoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<Customer> Get(int id)
        {
            return await _context.Customers.FindAsync(id);
        }
        public async Task Add(Customer entity)
        {
            await _context.Customers.AddAsync(entity);
        }
        public async Task Update(Customer entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public async Task<Customer> Delete(int id)
        {
            var entity = await _context.Customers.FindAsync(id);
            if (entity != null)
            {
                _context.Customers.Remove(entity);
                return entity;
            }
            return null;
        }

        //For token validate
        public async Task<Customer> GetUser(string email, string password)
        {
            return await _context.Customers.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
