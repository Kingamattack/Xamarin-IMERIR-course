// File: DetailsPage.xaml.cs
// Author: jordy
// Date: 21/1/2020

using tvshows.Models;
using tvshows.ViewModels;

using Xamarin.Forms;

namespace tvshows.Views
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Show show)
        {
            InitializeComponent();

            ((DetailViewModel)BindingContext).Show = show;
        }
    }
}