﻿// File: MainViewModel.cs
// Author: jordy
// Date: 20/1/2020

using GalaSoft.MvvmLight;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace tvshows.ViewModels
{
    public class MainViewModel : ViewModelBase
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

        public ICommand SearchCommand { get; private set; }

        private List<JsonShow> jsonShows;

        public MainViewModel()
        {
            jsonShows = new List<JsonShow>();
            Text = string.Empty;
            Shows = new ObservableCollection<Show>();
            SearchCommand = new Command<string>(async (string query) => await Search(query));
        }

        private async Task Search(string query)
        {
            try
            {
                if(query?.Length >= 3)
                {
                    jsonShows.Clear();
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
            catch (Exception)
            {

            }
        }
    }
}