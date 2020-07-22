using DemoWebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationDbContext _context;

        private ICustomerRepository _customers;
        private IProductRepository _products;
        private IOrderRepository _orders;

        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (_customers == null)
                {
                    _customers = new CustomerRepository(_context);
                }
                return _customers;
            }
        }
        public IProductRepository Products
        {
            get
            {
                if (_products == null)
                {
                    _products = new ProductRepository(_context);
                }
                return _products;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if(_orders==null)
                {
                    _orders = new OrderRepository(_context);
                }
                return _orders;
            }
        }
        
        public async Task<int> Save()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
