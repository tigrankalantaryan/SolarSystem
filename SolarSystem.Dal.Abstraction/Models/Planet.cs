using System.Collections;
using System.Collections.Generic;
using SolarSystem.Dal.Abstraction.Base;

namespace SolarSystem.Dal.Abstraction.Models
{
    public class Planet:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PlanetProperty> Properties { get; set; }
    }
}