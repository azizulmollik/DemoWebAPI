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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public OrdersController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _repositoryWrapper.Orders.GetAll();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _repositoryWrapper.Orders.Get(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            int i = 0;

            if (id != order.OrderId)
            {
                return BadRequest();
            }

            try
            {
                await _repositoryWrapper.Orders.Update(order);
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

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            int i = 0;
            try
            {
                await _repositoryWrapper.Orders.Add(order);
                i = await _repositoryWrapper.Save();
            }
            catch (Exception)
            {
                throw;
            }
            //return Ok(i);

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            int i = 0;
            var data = await _repositoryWrapper.Orders.Delete(id);
            if (data != null)
            {
                i = await _repositoryWrapper.Save();
                return Ok(i);
            }
            return NotFound();
        }

        // PUT: api/Orders/PutOrderStatus/5
        [HttpPut("PutOrderStatus/{id}")]
        public async Task<IActionResult> PutOrderStatus(int id, int statusId)
        {
            int i = 0;           

            try
            {
                var r =await _repositoryWrapper.Orders.UpdateStatus(id, statusId);                
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
    }
}
