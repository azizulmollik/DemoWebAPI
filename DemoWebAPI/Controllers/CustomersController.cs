using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoWebAPI.Models;
using DemoWebAPI.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace DemoWebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CustomersController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _repositoryWrapper.Customers.GetAll();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _repositoryWrapper.Customers.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            int i=0;

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            try
            {
                await _repositoryWrapper.Customers.Update(customer);
                i = await _repositoryWrapper.Save();
            }
            catch (DbUpdateConcurrencyException)
            {                
                if (i == 0)
                {
                    return NotFound();
                }
            }
            return Ok(i);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            int i = 0;
            try
            {
                await _repositoryWrapper.Customers.Add(customer);
                i = await _repositoryWrapper.Save();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(i);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            int i=0;
            var data = await _repositoryWrapper.Customers.Delete(id);
            if (data != null)
            {
                i = await _repositoryWrapper.Save();
                return Ok(i);
            }
            return NotFound();
        }
    }
}
