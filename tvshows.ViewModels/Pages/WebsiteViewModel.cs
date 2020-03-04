// File: WebsiteViewModel.cs
// Author: Jordy Kingama
// Date: 2/3/2020

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

using System.Windows.Input;

using tvshows.Services.Navigation;

using Xamarin.Forms;

namespace tvshows.ViewModels
{
    public class WebsiteViewModel : ViewModelBase
    {
        private string url;
        public string Url
        {
            get => url;
            set => Set(ref url, value);
        }

        public ICommand ClosePageCommand { get; private set; }

        private readonly INavigationService2 navigationService;

        public WebsiteViewModel()
        {
            ClosePageCommand = new Command(ClosePage);
            navigationService = SimpleIoc.Default.GetInstance<INavigationService2>();
        }

        private void ClosePage()
        {
            navigationService.GoBack(true);
        }
    }
}