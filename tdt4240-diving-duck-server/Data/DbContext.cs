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
        public DbSet<Score> Scores { get; set; }
        public DbSet<User> Users { get; set; }


        public MyContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
          
        }
    }
}

