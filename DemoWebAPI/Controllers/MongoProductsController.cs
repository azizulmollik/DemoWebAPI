using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DemoWebAPI.Repositories;
using DemoWebAPI.Models;

namespace DemoWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MongoProductsController : ControllerBase
    {
        private readonly IMongoProductRepository _mongoProductRepository;

        public MongoProductsController(IMongoProductRepository mongoProductRepository)
        {
            _mongoProductRepository = mongoProductRepository;
        }

        // GET: api/MProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MongoProduct>>> GetProducts()
        {            
            var products = await _mongoProductRepository.GetAll();
            return Ok(products);
        }

        // GET: api/MProducts/5
        [HttpGet("{id}")]       
        public async Task<ActionResult<MongoProduct>> GetProduct(string id)
        {
            var product = await _mongoProductRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // PUT: api/MProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(string id, MongoProduct product)
        {
            var data = await _mongoProductRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            await _mongoProductRepository.Update(id, product);
            return Ok(data);
        }

        // POST: api/MProducts
        [HttpPost]
        public async Task<ActionResult<MongoProduct>> PostProducts(MongoProduct product)
        {
            await _mongoProductRepository.Add(product);
            return Ok(product);
        }

        // DELETE: api/MProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MongoProduct>> DeleteProducts(string id)
        {            
            var data = await _mongoProductRepository.Get(id);
            if (data != null)
            {
                await _mongoProductRepository.Delete(id);
                return Ok(data);
            }
            return NotFound();
        }        
    }
}
