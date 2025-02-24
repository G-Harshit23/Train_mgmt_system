using Microsoft.EntityFrameworkCore;
using Trainapi.Models;

namespace Trainapi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Train> Trains { get; set; }
        public DbSet<User> users { get; set; }
    }
}
