using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.VisualBasic;
using SolarSystem.Core.Commands;
using SolarSystem.Dal.Abstraction.Models;
using SolarSystem.Dal.Abstraction.Repositories;

namespace SolarSystem.Core.ViewModels
{
    public class PlanetViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private ICommand _viewProperties;
        private ICommand _deletePlanet;
        private MainScreenViewModel _parentViewModel;
        private ObservableCollection<PlanetProperty> _planetProperties;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public ObservableCollection<PlanetProperty> PlanetProperties
        {
            get => _planetProperties;
            set
            {
                if (_planetProperties != value)
                {
                    _planetProperties = value;
                    OnPropertyChanged(nameof(PlanetProperties));
                }
            }
        }

        public ICommand ViewProperties
        {
            get => _viewProperties;
            set
            {
                if (Equals(value, _viewProperties)) return;
                _viewProperties = value;
                OnPropertyChanged(nameof(ViewProperties));
            }
        }

        public ICommand DeletePlanet
        {
            get => _deletePlanet;
            set
            {
                if (Equals(value, _deletePlanet)) return;
                _deletePlanet = value;
                OnPropertyChanged(nameof(DeletePlanet));
            }
        }

        public PlanetViewModel(Planet planet, MainScreenViewModel parentViewModel)
        {
            this._parentViewModel = parentViewModel;
            _id = planet.Id;
            _name = planet.Name;
            _planetProperties = new ObservableCollection<PlanetProperty>(planet.Properties??new List<PlanetProperty>());
            this.ViewProperties = new ViewPropertiesCommand(ExecuteGetPlanetProperties, CanExecuteMyMethod);
            this.DeletePlanet = new ViewPropertiesCommand(DeletePlanetBy, CanExecuteMyMethod);
        }

        public Planet Model => new Planet
        {
            Name = this.Name,
            Properties = this.PlanetProperties.ToList()
        };

        private bool CanExecuteMyMethod(object parameter)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            return Name != "";
        }

        private async void DeletePlanetBy(object parameter)
        {
            await this._parentViewModel.DeletePlanet(this.Id);
        }
        
        private async void ExecuteGetPlanetProperties(object parameter)
        {
            await this._parentViewModel.PopulatePlanetPropertiesById(this.Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}