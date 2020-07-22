using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoWebAPI.Data;
using DemoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll(bool? inStock)
        {
            if (inStock != null) //check availability 
            {
                return await _context.Products.Where(i => i.AvailableQuantity > 0).ToListAsync();               
            }
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> Get(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task Add(Product entity)
        {
            await _context.Products.AddAsync(entity);
        }
        public async Task Update(Product entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public async Task<Product> Delete(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity != null)
            {
                _context.Products.Remove(entity);
                return entity;
            }
            return null;
        }
    }
}
