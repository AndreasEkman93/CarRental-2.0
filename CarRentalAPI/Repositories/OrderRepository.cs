using CarRental.Models;
using CarRentalAPI.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data
{
    public class OrderRepository : IOrder
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await context.Orders.Include(o => o.Car).OrderBy(o => o.CustomerId).ThenByDescending(o => o.EndDate).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllSpecificCustomerAsync(string id)
        {
            return await context.Orders.Include(o => o.Car).Where(o => o.CustomerId == id).OrderByDescending(o => o.StartDate).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var order = await context.Orders.Include(a => a.Customer).Include(b => b.Car).FirstOrDefaultAsync(s => s.Id == id);
            if (order != null)
            {
                return order;
            }
            return null;
        }

        public async Task<List<DateOnly>> GetBookedDatesForCarAsync(int carId)
        {
            var orders = await context.Orders.Where(o => o.CarId == carId).ToListAsync();

            var bookedDates = new List<DateOnly>();

            foreach (var order in orders)
            {
                for (var date = order.StartDate; date <= order.EndDate; date = date.AddDays(1))
                {
                    bookedDates.Add(date);
                }
            }
            return bookedDates;
        }
    }
}
