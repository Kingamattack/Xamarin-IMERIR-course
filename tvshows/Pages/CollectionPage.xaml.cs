// File: CollectionPage.xaml.cs
// Author: Jordy Kingama
// Date: 24/2/2020

using System;

using tvshows.ViewModels;

using Xamarin.Forms;

namespace tvshows.Views
{
    public partial class CollectionPage : ContentPage
    {
        public CollectionPage()
        {
            InitializeComponent();
        }

        private void OnAppearing(object sender, EventArgs e)
        {
            ((CollectionViewModel)BindingContext).AppearingCommand.Execute(null);
        }
    }
}