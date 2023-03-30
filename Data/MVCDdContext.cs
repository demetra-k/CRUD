namespace CRUDApplication.Data
{
    using CRUDApplication.Models.Domain;
    using Microsoft.EntityFrameworkCore;

    public class MVCDdContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=PC\\SQLEXPRESS;Initial Catalog=MvcCrudDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }

        }

        public MVCDdContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>();
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
