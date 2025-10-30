using CarRental.Data;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder orderRepository;

        public OrderController(IOrder orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await orderRepository.GetAllAsync();
            if (!orders.Any())
            {
                return NotFound("No orders found");
            }
            return Ok(orders);
        }

        //GET /api/OrderController/customer/123
        [HttpGet("customer/{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllSpecificCustomerAsync(string id)
        {
            var orders = await orderRepository.GetAllSpecificCustomerAsync(id);
            if(!orders.Any())
            {
                return NotFound("No orders found for this customer");
            }
            return Ok(orders);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound("No order found");
            }
            return Ok(order);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            await orderRepository.AddAsync(order);

            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }


        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Car with ID {id} not found");
            }

            await orderRepository.DeleteAsync(order);
            return NoContent();
        }
    }
}
