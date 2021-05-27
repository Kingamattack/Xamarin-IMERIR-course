// File: WebsiteViewModel.cs
// Author: Jordy Kingama
// Date: 2/3/2020

using GalaSoft.MvvmLight;

using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace tvshows.ViewModels
{
    [QueryProperty(nameof(Url), "url")]
    public class WebsiteViewModel : ViewModelBase
    {
        private string url;
        public string Url
        {
            get => url;
            set => Set(ref url, value);
        }

        public ICommand ClosePageCommand { get; private set; }


        public WebsiteViewModel()
        {
            ClosePageCommand = new Command(async () => await ClosePage());
        }

        private async Task ClosePage()
        {
            await Shell.Current.GoToAsync("..", true);
        }
    }
}