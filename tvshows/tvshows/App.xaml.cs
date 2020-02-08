using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

using tvshows.Navigation;
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

            var navigationService = new NavigationService();
            navigationService.Configure("Details", typeof(DetailsPage));
            navigationService.Configure("Search", typeof(SearchPage));

            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            DependencyService.Register<IFavoriteService, FavoriteService>();

            var navigationPage = new NavigationPage(new HomePage());

            navigationService.Initialize(navigationPage);

            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}