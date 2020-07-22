using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoWebAPI.Data;
using DemoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders.Include(a=>a.OrderDetails).ToListAsync();            
        }

        public async Task<Order> Get(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Entry(order).Collection(s => s.OrderDetails).Load();

            return order;
        }

        public async Task Add(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;

            foreach (var orderDetail in order.OrderDetails)
            {
                _context.Entry(orderDetail).State = EntityState.Modified;                                
            }
        }

        public async Task<Order> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Entry(order).Collection(s => s.OrderDetails).Load();

            if (order != null)
            {
                _context.OrderDetails.RemoveRange(order.OrderDetails);
                _context.Orders.Remove(order);
                return order;
            }
            return null;
        }

        public async Task<int> UpdateStatus(int id, int statusId)
        {            
            var i = _context.Database.ExecuteSqlCommand("SP_UpdateOrderStatus @p0, @p1", parameters: new[] { id, statusId});
            return 0;
        }
    }
}
