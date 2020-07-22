using DemoWebAPI.Data;
using DemoWebAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Repositories
{
    public class MongoProductRepository : IMongoProductRepository
    {
        private readonly IMongoCollection<MongoProduct> _mongoProducts;

        public MongoProductRepository(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _mongoProducts = database.GetCollection<MongoProduct>(settings.ProductsCollectionName);
        }

        public async Task<List<MongoProduct>> GetAll()
        {
            return await _mongoProducts.Find(s => true).ToListAsync();
        }

        public async Task<MongoProduct> Get(string id)
        {
            return await _mongoProducts.Find<MongoProduct>(s => s.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task Add(MongoProduct product)
        {
            await _mongoProducts.InsertOneAsync(product);
        }

        public async Task Update(string id, MongoProduct product)
        {
            await _mongoProducts.ReplaceOneAsync(s => s.ProductId == id, product);
        }

        public async Task<MongoProduct> Delete(string id)
        {
            var product = await _mongoProducts.Find<MongoProduct>(s => s.ProductId == id).FirstOrDefaultAsync();
            if (product != null)
            {
                await _mongoProducts.DeleteOneAsync(s => s.ProductId == id);
                return product;
            }
            return null;
        }
    }
}
