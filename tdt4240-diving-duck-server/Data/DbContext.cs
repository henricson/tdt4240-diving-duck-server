using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using DivingDuckServer.Models;

namespace DivingDuckServer.Data
{
    public class ScoreContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }
        public DbSet<User> Users { get; set; }

        public string DbPath { get; }

        public ScoreContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "scoreboard.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}

