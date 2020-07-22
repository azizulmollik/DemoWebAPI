using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DemoWebAPI.Models;
using DemoWebAPI.Repositories;

namespace DemoWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProductsController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/Products        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(bool? inStock)
        {
            return await _repositoryWrapper.Products.GetAll(inStock);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var products = await _repositoryWrapper.Products.Get(id);

            if (products == null)
            {
                return NotFound();
            }
            return products;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Product product)
        {
            int i = 0;

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                await _repositoryWrapper.Products.Update(product);
                i = await _repositoryWrapper.Save();
            }            
            catch (DbUpdateConcurrencyException)
            {
                if (i == 0)
                {
                    return NotFound();
                    //return NoContent();
                }
            }
            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProducts(Product product)
        {
            int i = 0;
            try
            {
                await _repositoryWrapper.Products.Add(product);
                i = await _repositoryWrapper.Save();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(product);
            //return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProducts(int id)
        {
            int i = 0;
            var data = await _repositoryWrapper.Products.Delete(id);
            if (data != null)
            {
                i = await _repositoryWrapper.Save();
                return data;
            }
            return NotFound();
        }        
    }
}
