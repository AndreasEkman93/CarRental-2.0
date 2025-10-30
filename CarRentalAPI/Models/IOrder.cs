
using CarRental.Models;

namespace CarRental.Data
{
    public interface IOrder
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetAllSpecificCustomerAsync(string id);
        Task<List<DateOnly>> GetBookedDatesForCarAsync(int carId);
        Task<Order> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
