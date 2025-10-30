using System.Reflection.Metadata.Ecma335;
using CarRental.Data;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

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
            if(!cars.Any())
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
