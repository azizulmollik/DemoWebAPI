using DemoWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Repositories
{
    public interface IMongoProductRepository
    {
        Task<List<MongoProduct>> GetAll();
        Task<MongoProduct> Get(string id);
        Task Add(MongoProduct product);
        Task Update(string id, MongoProduct product);
        Task<MongoProduct> Delete(string id);
    }
}
