using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolarSystem.Dal.Abstraction.Models;
using SolarSystem.Dal.Sqlite;
using SolarSystem.Dal.Sqlite.Repositories;

namespace SolarSystem.MockData.Generator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await using var dbContext = new SolarDbContext();
            var repo = new PlanetRepository(dbContext);
            var planets = await repo.Get();
            foreach (var i in Enumerable.Range(1,10))
            {
                await repo.Add(new Planet
                {
                    Name = $"Planet Number {i}",
                    Properties = new List<PlanetProperty>
                    {
                        new PlanetProperty{Name = "DummyProperty", Value = $"Value 1 for planet Number {i}"},
                        new PlanetProperty{Name = "DummyProperty2", Value = $"Value 2 for planet Number {i}"}
                    }
                });
            }
            var planet1s = await repo.Get();
        }
    }
}
