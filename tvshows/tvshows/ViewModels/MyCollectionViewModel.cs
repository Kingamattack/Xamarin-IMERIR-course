// File: MyCollectionViewModel.cs
// Author: Jordy Kingama
// Date: 27/1/2020

using GalaSoft.MvvmLight;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

using tvshows.Models;
using tvshows.Services;

using Xamarin.Forms;

namespace tvshows.ViewModels
{
    public class MyCollectionViewModel : ViewModelBase
    {
        private ObservableCollection<Show> shows;
        public ObservableCollection<Show> Shows
        {
            get => shows;
            set => Set(ref shows, value);
        }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => Set(ref isBusy, value);
        }

        public ICommand GetShowsCommand => new Command(GetShows);

        private readonly FavoriteService favoriteService;

        private void GetShows()
        {
            try
            {
                IsBusy = true;

                var list = favoriteService.GetShows();

                Shows = new ObservableCollection<Show>(list);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(MyCollectionViewModel)}: {ex.StackTrace}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public MyCollectionViewModel()
        {
            IsBusy = false;
            Shows = new ObservableCollection<Show>();

            favoriteService = new FavoriteService();
        }
    }
}