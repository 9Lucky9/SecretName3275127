using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System;
using System.Windows;
using VitaLabManager.Services.Product;
using VitaLabManager.MVVM.ViewModels;
using VitaLabManager.Services.Order;
using VitaLabManager.Services.User;
using VitaLabManager.MVVM.Views;
using VitaLabManager.Services;
using VitaLabManager.Services.Authorization;
using VitaLabManager.Services.Navigation;
using VitaLabManager.MVVM.ViewModels.MainWindowViewModel;
using VitaLabManager.Services.ISessionContext;

namespace VitaLabManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            
        }

        private void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IConfiguration>(_ => Configuration);
            services.AddSingleton<Func<Type, ViewModel>>(
                serviceProvider =>
                viewModelType =>
                (ViewModel)serviceProvider.GetRequiredService(viewModelType));
            services.AddSingleton<IVitaLabApiWrapper, VitaLabApiWrapper>();

            //Services
            services.AddSingleton<ISessionContext, SessionContext>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<AuthorizationService>();
            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IOrderManager, OrderManager>();


            services.AddSingleton<ProductControl>();
            services.AddSingleton<ProductControlViewModel>();

            services.AddSingleton<CustomerControl>();
            services.AddSingleton<CustomerControlViewModel>();

            services.AddSingleton<ProductOrderControl>();
            services.AddSingleton<ProductOrderControlViewModel>();

            services.AddSingleton<BasketControl>();
            services.AddSingleton<BasketControlViewModel>();

            services.AddSingleton<UsersControl>();
            services.AddSingleton<UsersControlViewModel>();

            services.AddSingleton<LoginWindowViewModel>();
            services.AddSingleton<LoginWindow>(x => new LoginWindow()
            {
                DataContext = x.GetRequiredService<LoginWindowViewModel>()
            });

            services.AddSingleton<MainWindowViewModelFactory>();

            //services.AddSingleton<MainWindowViewModel>();
            //services.AddSingleton<MainWindow>(x => new MainWindow()
            //{
            //    DataContext = x.GetRequiredService<MainWindowViewModel>()
            //});
        }
    }
}
