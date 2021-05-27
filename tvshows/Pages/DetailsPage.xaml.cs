// File: DetailsPage.xaml.cs
// Author: jordy
// Date: 21/1/2020

using System;

using tvshows.ViewModels;

using Xamarin.Forms;

namespace tvshows.Views
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage()
        {            
            InitializeComponent();
        }

        private void OnAppearing(object sender, EventArgs e)
        {
            ((DetailViewModel)BindingContext).AppearingCommand.Execute(null);
        }
    }
}