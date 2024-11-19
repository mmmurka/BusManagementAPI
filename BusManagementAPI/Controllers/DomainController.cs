using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusManagementAPI.Data;

namespace BusManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DomainController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DomainController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDomainModel()
        {
            var drivers = await _context.Drivers.ToListAsync();
            var buses = await _context.Buses.ToListAsync();
            var routes = await _context.Routes.ToListAsync();
            var trips = await _context.Trips.ToListAsync();

            var domainModel = new
            {
                Drivers = drivers,
                Buses = buses,
                Routes = routes,
                Trips = trips
            };

            return Ok(domainModel);
        }
    }
}
