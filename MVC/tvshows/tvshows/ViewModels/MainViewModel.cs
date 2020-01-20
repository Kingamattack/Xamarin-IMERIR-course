// File: MainViewModel.cs
// Author: jordy
// Date: 20/1/2020

using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        public MainViewModel()
        {
            Text = string.Empty;
            Shows = new ObservableCollection<Show>();
            SearchCommand = new Command<string>(async (string query) => await Search(query));
        }

        private async Task Search(string query)
        {
            List<Show> shows = new List<Show>();

            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync($"http://api.tvmaze.com/search/shows?q={query}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Value: {data}");

                    var list = JsonConvert.DeserializeObject<List<JsonShow>>(data);

                    List<Show> s = new List<Show>();

                    foreach (var item in list)
                    {
                        s.Add(item.Show);
                    }

                    Shows = new ObservableCollection<Show>(s);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}