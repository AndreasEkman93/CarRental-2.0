using CarRental.Models;
using CarRentalAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data
{
    public class CarRepository : ICar
    {
        private readonly ApplicationDbContext context;

        public CarRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Car car)
        {
            context.Cars.Add(car);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Car car)
        {
            context.Cars.Remove(car);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await context.Cars.OrderBy(c => c.Model).ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Car car)
        {
            context.Entry(car).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        public async Task<bool> CarExistsAsync(int id)
        {
            return await context.Cars.AnyAsync(e => e.Id == id);
        }
    }
}
