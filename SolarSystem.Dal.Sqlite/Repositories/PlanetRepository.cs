using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SolarSystem.Dal.Abstraction.Models;
using SolarSystem.Dal.Abstraction.Repositories;
using SolarSystem.Dal.Sqlite.Base;

namespace SolarSystem.Dal.Sqlite.Repositories
{
    public class PlanetRepository : BaseRepository<Planet, int>, IPlanetRepository
    {
        public PlanetRepository(SolarDbContext dbContext) : base(dbContext)
        {
        }

        public override Task<Planet> Get(int key)
        {
            return _dbContext.Planets.Include(inc => inc.Properties).SingleOrDefaultAsync(ent => ent.Id == key);
        }
    }
}