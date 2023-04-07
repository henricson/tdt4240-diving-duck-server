using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using DivingDuckServer.Models;

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
                string connectionString = Environment.GetEnvironmentVariable("DefaultConnection")!;
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string not found in environment variables.");
                }
                optionsBuilder.UseNpgsql(connectionString);
            }
            else
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}

