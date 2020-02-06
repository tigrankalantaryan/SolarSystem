using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using SolarSystem.Core.ViewModels;
using SolarSystem.Dal.Abstraction.Repositories;

namespace SolarSystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly MainScreenViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _planetRepository = App.ServiceProvider.GetService<IPlanetRepository>();
            _viewModel = new MainScreenViewModel(_planetRepository);
            DataContext = _viewModel;
            this.Loaded += async (sender, args) =>
            {
                await _viewModel.Load();
                this.UpdateLayout();
            };
        }

        private void AddPlanetClick(object sender, RoutedEventArgs e)
        {
            var createPlanetWindow = new CreateNewPlanet();
            createPlanetWindow.Closing += async (o, args) => await _viewModel.Load();
            createPlanetWindow.ShowDialog();
        }
    }
}
