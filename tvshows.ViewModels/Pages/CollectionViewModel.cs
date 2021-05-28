// File: CollectionViewModel.cs
// Author: Jordy Kingama
// Date: 27/1/2020

using GalaSoft.MvvmLight.Ioc;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using tvshows.Models;
using tvshows.Models.Entities;
using tvshows.Services;

using Xamarin.Forms;

namespace tvshows.ViewModels
{
    public class CollectionViewModel : BaseViewModel
    {
        #region Properties

        private ObservableCollection<Showgroup> shows;
        public ObservableCollection<Showgroup> Shows
        {
            get => shows;
            set => Set(ref shows, value);
        }        

        private BaseShow selectedShow;
        public BaseShow SelectedShow
        {
            get => selectedShow;
            set
            {
                Set(ref selectedShow, value);
                OpenShowDetailsCommand.Execute(selectedShow);
            }
        }

        #endregion

        #region Commands

        public ICommand GetShowsCommand { get; private set; }
        public ICommand AppearingCommand { get; private set; }
        public ICommand OpenSearchCommand { get; private set; }
        public ICommand OpenShowDetailsCommand { get; private set; }

        #endregion

        #region Services

        private readonly IFavoriteService favoriteService;
        private readonly IFirebaseService firebaseService;
        private readonly IShowService showService;

        #endregion        

        public CollectionViewModel()
        {
            IsBusy = false;
            Shows = new ObservableCollection<Showgroup>();

            GetShowsCommand = new Command<List<Show>>(GetShows);
            AppearingCommand = new Command(async () => await Appearing());
            OpenSearchCommand = new Command(async () => await OpenSearchPage());
            OpenShowDetailsCommand = new Command<Show>(async (show) => await OpenDetailsPage(show));

            firebaseService = DependencyService.Get<IFirebaseService>();
            favoriteService = SimpleIoc.Default.GetInstance<IFavoriteService>();
            showService = SimpleIoc.Default.GetInstance<IShowService>();

            MessagingCenter.Subscribe<List<Show>>(this, "GetShows", (shows) =>
            {
                GetShows(shows);
            });
        }

        #region Methods

        private async Task Appearing()
        {
            IsBusy = true;
            var myShows = new List<Show>();
            var ids = favoriteService.GetShowIds();

            foreach (int id in ids)
            {
                var _show = await showService.GetShow(id);
                myShows.Add(_show);
            }

            GetShows(myShows);
        }

        private async Task OpenDetailsPage(Show show)
        {
            await Shell.Current.GoToAsync($"DetailsPage?show={show.Id}", true);

        }

        private void GetShows(List<Show> shows)
        {
            try
            {
                if (shows == null || !shows.Any())
                {
                    return;
                }

                var group = shows
                    .GroupBy(s => s.Name.First())
                    .OrderBy(g => g.Key);

                var groups = new List<Showgroup>();

                foreach (var grp in group)
                {
                    var showGroup = new Showgroup(grp.Key.ToString(), grp.ToList());
                    groups.Add(showGroup);
                }

                Shows = new ObservableCollection<Showgroup>(groups);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(CollectionViewModel)}: {ex.StackTrace}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OpenSearchPage()
        {
            await Shell.Current.GoToAsync("SearchPage", true);
        }

        #endregion
    }
}