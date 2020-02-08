// File: MyCollectionViewModel.cs
// Author: Jordy Kingama
// Date: 27/1/2020

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

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
        #region Properties

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

        private Show selectedShow;
        public Show SelectedShow
        {
            get => selectedShow;
            set
            {
                if (value != null)
                {
                    Set(ref selectedShow, value);

                    navigationService.NavigateTo("Details", selectedShow);
                }
            }
        }

        #endregion

        #region Commands

        public ICommand GetShowsCommand { get; private set; }
        public ICommand OpenShowDetailsCommand { get; private set; }

        #endregion

        #region Services

        private readonly IFavoriteService favoriteService;
        private readonly INavigationService navigationService;

        #endregion        

        public MyCollectionViewModel()
        {
            IsBusy = false;
            Shows = new ObservableCollection<Show>();

            GetShowsCommand = new Command(GetShows);
            OpenShowDetailsCommand = new Command<Show>(OpenShowDetails);

            favoriteService = DependencyService.Get<IFavoriteService>();
            navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
        }

        private void OpenShowDetails(Show show)
        {
            throw new NotImplementedException();
        }

        #region Methods

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

        #endregion
    }
}