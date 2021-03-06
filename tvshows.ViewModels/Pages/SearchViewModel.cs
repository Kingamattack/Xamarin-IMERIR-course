﻿// File: SearchViewModel.cs
// Author: jordy
// Date: 20/1/2020

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using tvshows.Models;
using tvshows.Services;

using Xamarin.Forms;

namespace tvshows.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private string text;
        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        private ObservableCollection<Show> shows;
        public ObservableCollection<Show> Shows
        {
            get => shows;
            set => Set(ref shows, value);
        }

        private Show selectedShow;
        public Show SelectedShow
        {
            get => selectedShow;
            set
            {
                if(value != null)
                {
                    Set(ref selectedShow, value);

                    navigationService.NavigateTo("Details", selectedShow);
                }
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => Set(ref isBusy, value);
        }

        private readonly IShowService showService;
        private readonly INavigationService navigationService;

        public ICommand SearchCommand { get; private set; }

        public SearchViewModel()
        {
            IsBusy = false;
            Text = string.Empty;
            Shows = new ObservableCollection<Show>();

            showService = SimpleIoc.Default.GetInstance<IShowService>();
            navigationService = SimpleIoc.Default.GetInstance<INavigationService>();

            SearchCommand = new Command<string>(async (string query) => await Search(query));
        }

        private async Task Search(string query)
        {
            try
            {
                IsBusy = true;

                if (query?.Length >= 3)
                {
                    var shows = await showService.GetShows(query);
                    Shows = new ObservableCollection<Show>(shows);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{nameof(SearchViewModel)}: {e.StackTrace}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}