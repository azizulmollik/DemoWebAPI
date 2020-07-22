using DemoWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();
        Task<Order> Get(int id);
        Task Add(Order entity);
        Task Update(Order entity);
        Task<Order> Delete(int id);
        Task<List<Customer>> GetCustomerList(bool hasOrder);
        Task<int> UpdateStatus(int id, int statusId);
    }
}
