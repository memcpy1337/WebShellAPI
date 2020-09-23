using Microsoft.EntityFrameworkCore;
using WebApplication10.Model;

namespace WebApplication10.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Values> Values { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated(); 
        }
    }
}