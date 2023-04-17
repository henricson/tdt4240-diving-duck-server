using DivingDuckServer.Models;
using Microsoft.EntityFrameworkCore;

namespace DivingDuckServer.Data
{
    public class MyContext : DbContext
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment _env;

        public DbSet<Score> Scores { get; set; }
        public DbSet<User> Users { get; set; }


        public MyContext(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_env.IsProduction())
            {
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_AZURE_POSTGRESQL_CONNECTIONSTRING"));
            }
            else
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Scores)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
        }
    }
}

