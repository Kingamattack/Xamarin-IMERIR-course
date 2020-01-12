using Newtonsoft.Json;

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
        public ObservableCollection<string> Values { get; set; } = new ObservableCollection<string>();
        

        private readonly List<string> movies = new List<string>
        {
            "Avengers",
            "Forest Gump",
            "Spiderman",
            "Superman",
            "Black Panther",
            "Venom",
            "Iron man",
            "Captain America",
            "Dr. Strange",
            "Thor",
            "Hulk",
            "She Hulk",
            "Gardiens"
        };

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchBar = (SearchBar)sender;

            if(searchBar.Text.Length >= 3)
            {
                await GetData(searchBar.Text);
            }
        }

        private async Task GetData(string query)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"http://api.tvmaze.com/search/shows?q={query}");

            if(response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Value: {data}");

                var list = JsonConvert.DeserializeObject<List<JsonShow>>(data);

                var show = list[0];
            }            
        }
    }
}
