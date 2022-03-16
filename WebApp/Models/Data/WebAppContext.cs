using Microsoft.EntityFrameworkCore;

namespace WebApp.Models.Data
{
    public class WebAppContext : DbContext
    {
        public WebAppContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
