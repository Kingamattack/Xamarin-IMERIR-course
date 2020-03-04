// File: WebsitePage.xaml.cs
// Author: Jordy Kingama
// Date: 2/3/2020

using tvshows.ViewModels;

using Xamarin.Forms;

namespace tvshows.Views
{
    public partial class WebsitePage : ContentPage
    {
        public WebsitePage(string url)
        {
            InitializeComponent();

            ((WebsiteViewModel)BindingContext).Url = url;
        }
    }
}