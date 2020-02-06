using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SolarSystem.Dal.Abstraction.Repositories;
using SolarSystem.Dal.Sqlite;
using SolarSystem.Dal.Sqlite.Repositories;

namespace SolarSystem.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        private IServiceCollection services { get; }

        public App()
        {
            services = new ServiceCollection();
            services.AddDbContext<SolarDbContext>();
            services.AddTransient<IPlanetRepository, PlanetRepository>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
