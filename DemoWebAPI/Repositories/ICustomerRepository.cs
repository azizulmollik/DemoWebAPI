using DemoWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();
        Task<Customer> Get(int id);
        Task Add(Customer entity);
        Task Update(Customer entity);
        Task<Customer> Delete(int id);
        Task<Customer> GetUser(string email, string password);        
    }
}
