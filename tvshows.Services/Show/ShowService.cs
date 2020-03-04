// File: ShowService.cs
// Author: Jordy Kingama
// Date: 3/3/2020

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

using tvshows.Models;
using tvshows.Models.Entities;

namespace tvshows.Services
{
    public class ShowService : IShowService
    {
        private readonly HttpClient httpClient;
        private const string baseUrl = "http://api.tvmaze.com";

        public ShowService()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task<Show> GetShow(int id)
        {
            var show = new Show();

            try
            {
                var response = await httpClient.GetAsync($"/shows/{id}?embed[]=cast&embed[]=episodes");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    Debug.Write(data);
                    show = JsonConvert.DeserializeObject<Show>(data);
                }

                return show;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.StackTrace);
                throw ex;
            }
        }

        public async Task<List<Show>> GetShows(string query)
        {
            var shows = new List<Show>();

            try
            {
                var response = await httpClient.GetAsync($"/search/shows?q={query}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    Debug.Write(data);
                    var jsonShows = JsonConvert.DeserializeObject<List<JsonShow>>(data);

                    foreach (var item in jsonShows)
                    {
                        shows.Add(item.Show);
                    }
                }

                return shows;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.StackTrace);
                throw ex;
            }
        }
    }
}