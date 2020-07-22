using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using DemoWebAPI.Data;
using DemoWebAPI.Models;
using Microsoft.Data.SqlClient;
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

        public async Task<List<Customer>> GetCustomerList(bool hasOrder)
        {
            return await _context.Customers.FromSqlRaw<Customer>("SP_CustomerListByHasOrder @p0", hasOrder).ToListAsync();            
        }

        public async Task<int> UpdateStatus(int id, int status)
        {            
            DbCommand cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "dbo.SP_UpdateOrderStatus";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@orderId", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@status", SqlDbType.Int) { Value = status });
            //cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt) { Direction = ParameterDirection.Output });

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            return await cmd.ExecuteNonQueryAsync();
        }
       
    }
}
