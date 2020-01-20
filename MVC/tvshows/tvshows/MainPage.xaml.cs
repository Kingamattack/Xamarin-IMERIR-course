using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace tvshows
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Show> Shows { get; set; } = new ObservableCollection<Show>();

        public MainPage()
        {
            InitializeComponent();
        }

        private async Task GetData(string query)
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

                    foreach (var item in list)
                    {
                        Shows.Add(item.Show);
                    }

                    //Shows = new ObservableCollection<Show>(shows);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
