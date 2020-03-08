// File: MainPage.xaml.cs
// Author: Jordy Kingama
// Date: 24/2/2020

using System;

using tvshows.ViewModels;

using Xamarin.Forms;

namespace tvshows.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnAppearing(object sender, EventArgs e)
        {
            ((MainViewModel)BindingContext).AppearingCommand.Execute(null);
        }
    }
}