using GalaSoft.MvvmLight.Ioc;

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
            navigationService.Configure("Main", typeof(MainPage));
            navigationService.Configure("Website", typeof(WebsitePage));

            if(!SimpleIoc.Default.IsRegistered<INavigationService>())
                SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            if (!SimpleIoc.Default.IsRegistered<IFavoriteService>())
                SimpleIoc.Default.Register<IFavoriteService, FavoriteService>();

            if (!SimpleIoc.Default.IsRegistered<IShowService>())
                SimpleIoc.Default.Register<IShowService, ShowService>();

            var navigationPage = new NavigationPage(new MainPage())
            {
                BarTextColor = Color.White,
                BarBackgroundColor = (Color)Current.Resources["ThemeColor"]
            };

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