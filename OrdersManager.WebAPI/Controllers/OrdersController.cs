using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Entities;

namespace OrdersManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ApplicationDbContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Orders
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            _logger.LogInformation("Getting all orders");

            var orders = await _context.Orders.ToListAsync();

            _logger.LogInformation($"Returning {orders.Count} orders");

            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            _logger.LogInformation($"Getting order with id {id}");

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                _logger.LogWarning($"Order with id {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Returning order with id {id}");

            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.OrderId)
            {
                _logger.LogWarning($"Order id {id} does not match order id {order.OrderId}");
                return Problem(detail: "Order id does not match with the order id", statusCode: StatusCodes.Status400BadRequest, title: "Error");
            }

            var orderToUpdate = await _context.Orders.FindAsync(id);
            if (orderToUpdate == null)
            {
                _logger.LogWarning($"Order with id {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Updating order with id {id}");

            orderToUpdate.CustomerName = order.CustomerName;
            orderToUpdate.OrderDate = order.OrderDate;
            orderToUpdate.OrderNumber = order.OrderNumber;
            orderToUpdate.TotalAmount = order.TotalAmount;

            try
            {
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Order with id {id} updated");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _logger.LogInformation("Creating a new order");

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Order with id {order.OrderId} created");

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            _logger.LogInformation($"Deleting order with id {id}");

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                _logger.LogWarning($"Order with id {id} not found");
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Order with id {id} deleted");

            return NoContent();
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
