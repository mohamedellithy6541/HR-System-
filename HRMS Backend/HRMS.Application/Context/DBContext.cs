using HRMS.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HRMS.Application
{
	public class DBContext : IdentityDbContext<AppUser>
	{
        
        public DBContext() : base()
		{ }
        public DBContext(DbContextOptions<DBContext> options)
           : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Attendance> Attendances { get; set; }
		public DbSet<GeneralSettings> GeneralSettings { get; set; }
		public DbSet<SeasonalVacation> SeasonalVacations { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

		{
			optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=HRMS;Trusted_Connection=True;TrustServerCertificate=True;");
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}