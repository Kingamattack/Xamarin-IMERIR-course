using GalaSoft.MvvmLight.Ioc;

using tvBaseShows.Services;

using tvshows.Services;
using tvshows.Views;

using Xamarin.Forms;

namespace tvshows
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterRoutes();
            RegisterServices();
           
            MainPage = new MainShell();            
        }

        private void RegisterServices()
        {
            if (!SimpleIoc.Default.IsRegistered<IFavoriteService>())
                SimpleIoc.Default.Register<IFavoriteService, FavoriteService>();

            if (!SimpleIoc.Default.IsRegistered<IShowService>())
                SimpleIoc.Default.Register<IShowService, ShowService>();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(CollectionPage), typeof(CollectionPage));
            Routing.RegisterRoute(nameof(WebsitePage), typeof(WebsitePage));
        }
    }
}