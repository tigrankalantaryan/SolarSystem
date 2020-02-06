using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SolarSystem.Core.Commands;
using SolarSystem.Dal.Abstraction.Models;
using SolarSystem.Dal.Abstraction.Repositories;
using SolarSystem.Dal.Sqlite;

namespace SolarSystem.Core.ViewModels
{
    public class MainScreenViewModel : INotifyPropertyChanged
    {
        private readonly IPlanetRepository _planetRepository;
        private ICommand _addPlanet;
        public ObservableCollection<PlanetViewModel> Planets { get; set; } = new ObservableCollection<PlanetViewModel>();

        public ObservableCollection<PlanetProperty> PlanetProperties { get; set; } = new ObservableCollection<PlanetProperty>();

        public MainScreenViewModel(IPlanetRepository planetRepository)
        {
            _planetRepository = planetRepository;
        }

        public ICommand AdPlanet
        {
            get => _addPlanet;
            set
            {
                if (Equals(value, _addPlanet)) return;
                _addPlanet = value;
                OnPropertyChanged(nameof(DeletePlanet));
            }
        }

        public async Task Load()
        {
            var planets = await _planetRepository.Get();
            Planets.Clear();
            foreach (var planet in planets)
            {
                Planets.Add(new PlanetViewModel(planet, this));
                OnPropertyChanged(nameof(Planets));
            }
        }

        public async Task PopulatePlanetPropertiesById(int planetId)
        {
            var planet = await this._planetRepository.Get(planetId);
            var planetProperties = planet.Properties;
            PlanetProperties.Clear();
            foreach (var planetProperty in planetProperties)
            {
                PlanetProperties.Add(planetProperty);
            }

            OnPropertyChanged(nameof(PlanetProperties));
        }

        public async Task DeletePlanet(int planetId)
        {
            var planet = await this._planetRepository.Delete(planetId);
            await Load();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}