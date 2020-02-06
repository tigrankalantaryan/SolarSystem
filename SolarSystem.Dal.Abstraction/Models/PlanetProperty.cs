using SolarSystem.Dal.Abstraction.Base;

namespace SolarSystem.Dal.Abstraction.Models
{
    public class PlanetProperty : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int PlanetId { get; set; }

        public override string ToString()
        {
            return $"Name : {this.Name}, Value : {this.Value}";
        }
    }
}