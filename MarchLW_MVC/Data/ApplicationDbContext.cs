using MarchLW_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MarchLW_MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Rides> Rides { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<TicketDetails> TicketDetails { get; set; }
        public DbSet<TicketRides> TicketRides { get; set; }

    }
}
