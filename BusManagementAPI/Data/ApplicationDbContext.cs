using Microsoft.EntityFrameworkCore;

namespace BusManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Trip> Trips { get; set; }
    }

    public class Driver
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string License { get; set; }
    }

    public class Bus
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }

    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartStop { get; set; }
        public string EndStop { get; set; }
    }

    public class Trip
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int BusId { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string Status { get; set; }
    }
}
