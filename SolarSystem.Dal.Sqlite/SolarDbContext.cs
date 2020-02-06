using System;
using Microsoft.EntityFrameworkCore;
using SolarSystem.Dal.Abstraction.Models;

namespace SolarSystem.Dal.Sqlite
{
    public class SolarDbContext:DbContext
    {
        public DbSet<Planet> Planets { get; set; }
        public DbSet<PlanetProperty> PlanetProperties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Solar.db;");
        }
    }
}
