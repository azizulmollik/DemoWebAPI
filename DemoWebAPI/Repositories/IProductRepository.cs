using DemoWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll(bool? inStock);
        Task<Product> Get(int id);
        Task Add(Product entity);
        Task Update(Product entity);
        Task<Product> Delete(int id);
    }
}
