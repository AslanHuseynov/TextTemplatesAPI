using Company.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Persistence.DB
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateAuditTrail> AuditTrails { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-THVOU75\\MSSQLSERVER02;Database=TextTemplatesDB5;Trusted_Connection=true;TrustServerCertificate=true;", b => b.MigrationsAssembly("API"));
        }
    }
}
