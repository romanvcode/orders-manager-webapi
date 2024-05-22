using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Entities;

namespace OrdersManager.WebAPI.Controllers
{
    [Route("api/orders/{orderId}/items")]
    public class OrderItemsController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderItemsController> _logger;

        public OrderItemsController(ApplicationDbContext context, ILogger<OrderItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Orders/5/Items
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems(Guid orderId)
        {
            _logger.LogInformation($"Getting all order items for order with id {orderId}");

            var orderItems = await _context.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync();

            return Ok(orderItems);
        }

        // GET: api/Orders/5/Items/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderItem>> GetOrderItem(Guid orderId, Guid id)
        {
            _logger.LogInformation($"Getting order item with id {id} for order with id {orderId}");
            var orderItem = await _context.OrderItems.Where(oi => oi.OrderId == orderId).Where(oi => oi.OrderItemId == id).FirstOrDefaultAsync();

            if (orderItem == null)
            {
                _logger.LogWarning($"Order item with id {id} for order with id {orderId} not found");
                return NotFound();
            }

            return Ok(orderItem);
        }

        // PUT: api/OrderItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutOrderItem(Guid id, OrderItem orderItem)
        {
            if (id != orderItem.OrderItemId)
            {
                _logger.LogWarning($"Order item id {id} does not match the order item id {orderItem.OrderItemId}");
                return Problem(detail: "Order item id does not match the order item id", statusCode: StatusCodes.Status400BadRequest, title: "Error");
            }

            var orderItemToUpdate = await _context.OrderItems.FindAsync(id);
            if (orderItemToUpdate == null)
            {
                _logger.LogWarning($"Order item with id {id} not found");
                return NotFound();
            }

            orderItemToUpdate.Quantity = orderItem.Quantity;
            orderItemToUpdate.UnitPrice = orderItem.UnitPrice;
            orderItemToUpdate.TotalPrice = orderItem.TotalPrice;

            try
            {
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Order item with id {id} updated");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
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

        // POST: api/OrderItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem orderItem)
        {
            _logger.LogInformation("Creating a new order item");

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Order item with id {orderItem.OrderItemId} created");

            return CreatedAtAction("GetOrderItem", new { id = orderItem.OrderItemId }, orderItem);
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
        {
            _logger.LogInformation($"Deleting order item with id {id}");

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                _logger.LogWarning($"Order item with id {id} not found");
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Order item with id {id} deleted");

            return NoContent();
        }

        private bool OrderItemExists(Guid id)
        {
            return _context.OrderItems.Any(e => e.OrderItemId == id);
        }
    }
}
