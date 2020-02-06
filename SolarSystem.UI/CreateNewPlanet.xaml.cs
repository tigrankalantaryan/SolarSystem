using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using SolarSystem.Core.ViewModels;
using SolarSystem.Dal.Abstraction.Models;
using SolarSystem.Dal.Abstraction.Repositories;

namespace SolarSystem.UI
{
    /// <summary>
    /// Interaction logic for CreateNewPlanet.xaml
    /// </summary>
    public partial class CreateNewPlanet : Window
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly PlanetViewModel _planet;

        public CreateNewPlanet()
        {
            InitializeComponent();
            _planetRepository = App.ServiceProvider.GetService<IPlanetRepository>();
            _planet = new PlanetViewModel(new Planet(), null);
            this.DataContext = _planet;
        }

        private async void SavePlanetClick(object sender, RoutedEventArgs e)
        {
            await _planetRepository.Add(_planet.Model);
            this.Close();
        }
    }
}
