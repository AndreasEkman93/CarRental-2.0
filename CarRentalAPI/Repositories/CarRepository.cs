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
        private bool CarExists(int id)
        {
            return context.Cars.Any(e => e.Id == id);
        }
    }


public static class CarEndpoints
{
	public static void MapCarEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Car").WithTags(nameof(Car));

        group.MapGet("/", () =>
        {
            return new [] { new Car() };
        })
        .WithName("GetAllCars")
        .WithOpenApi();

        group.MapGet("/{id}", (int id) =>
        {
            //return new Car { ID = id };
        })
        .WithName("GetCarById")
        .WithOpenApi();

        group.MapPut("/{id}", (int id, Car input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateCar")
        .WithOpenApi();

        group.MapPost("/", (Car model) =>
        {
            //return TypedResults.Created($"/api/Cars/{model.ID}", model);
        })
        .WithName("CreateCar")
        .WithOpenApi();

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new Car { ID = id });
        })
        .WithName("DeleteCar")
        .WithOpenApi();
    }
}}
