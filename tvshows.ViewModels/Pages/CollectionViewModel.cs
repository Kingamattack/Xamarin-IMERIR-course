﻿// File: CollectionViewModel.cs
// Author: Jordy Kingama
// Date: 27/1/2020

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

using tvshows.Models;
using tvshows.Models.Entities;
using tvshows.Services;

using Xamarin.Forms;

namespace tvshows.ViewModels
{
    public class CollectionViewModel : ViewModelBase
    {
        #region Properties

        private ObservableCollection<Showgroup> shows;
        public ObservableCollection<Showgroup> Shows
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
                Set(ref selectedShow, value);
                OpenDetailsPage(value);
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
        private readonly INavigationService navigationService;

        #endregion        

        public CollectionViewModel()
        {
            IsBusy = false;
            Shows = new ObservableCollection<Showgroup>();

            GetShowsCommand = new Command(GetShows);
            AppearingCommand = new Command(Appearing);
            OpenSearchCommand = new Command(OpenSearchPage);
            OpenShowDetailsCommand = new Command<Show>(OpenDetailsPage);

            favoriteService = SimpleIoc.Default.GetInstance<IFavoriteService>();
            navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
        }        

        #region Methods

        private void Appearing()
        {
            GetShows();
        }

        private void OpenDetailsPage(Show show)
        {
            navigationService.NavigateTo("Details", show);
        }

        private void GetShows()
        {
            try
            {
                IsBusy = true;

                var shows = favoriteService.GetShows();
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

        private void OpenSearchPage()
        {
            navigationService.NavigateTo("Search");
        }

        #endregion
    }
}