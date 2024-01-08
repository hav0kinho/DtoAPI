using DtoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DtoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
