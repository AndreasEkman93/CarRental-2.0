using CarRental.Models;
using Microsoft.AspNetCore.Identity;

namespace CarRentalAPI.Data
{
    public class ApplicationUser : IdentityUser
    {
        public List<Order>? Orders { get; set; }
        public string? Name { get; set; }
    }
}
