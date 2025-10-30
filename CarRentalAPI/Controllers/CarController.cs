using System.Reflection.Metadata.Ecma335;
using CarRental.Data;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICar carRepository;

        public CarController(ICar carRepository)
        {
            this.carRepository = carRepository;
        }
        // GET: api/<CarController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars = await carRepository.GetAllAsync();
            if (!cars.Any())
            {
                return NotFound("No cars found");
            }
            return Ok(cars);
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await carRepository.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound("No car found");
            }
            return Ok(car);
        }

        // POST api/<CarController>
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            await carRepository.AddAsync(car);

            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest("ID mismatch");
            }


            try
            {
                await carRepository.UpdateAsync(car);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await carRepository.CarExistsAsync(id))
                {
                    return NotFound($"Car with ID {id} not found");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var car = await carRepository.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound($"Car with ID {id} not found");
            }

            await carRepository.DeleteAsync(car);
            return NoContent();
        }
    }
}
