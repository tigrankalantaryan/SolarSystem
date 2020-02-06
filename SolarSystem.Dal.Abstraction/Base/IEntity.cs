namespace SolarSystem.Dal.Abstraction.Base
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}