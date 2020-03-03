// File: SearchViewModel.cs
// Author: jordy
// Date: 20/1/2020

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

using tvshows.Models;
using tvshows.Models.Entities;
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

        private readonly INavigationService navigationService;
        private readonly IShowService showService;

        public ICommand SearchCommand { get; private set; }

        public SearchViewModel()
        {
            IsBusy = false;
            Text = string.Empty;
            Shows = new ObservableCollection<Show>();
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
                    var jsonShows = new List<JsonShow>();
                    var httpClient = new HttpClient();

                    var response = await httpClient.GetAsync($"http://api.tvmaze.com/search/shows?q={query}");

                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();

                        jsonShows = JsonConvert.DeserializeObject<List<JsonShow>>(data);

                        List<Show> s = new List<Show>();

                        foreach (var item in jsonShows)
                        {
                            s.Add(item.Show);
                        }

                        Shows = new ObservableCollection<Show>(s);
                    }
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