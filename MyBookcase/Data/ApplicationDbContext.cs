using Microsoft.EntityFrameworkCore;
using MyBookcase.Models.Entities;

namespace MyBookcase.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<Loan> Loans { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
