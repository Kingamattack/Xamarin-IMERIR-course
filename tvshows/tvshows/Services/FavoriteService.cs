// File: FavoriteService.cs
// Author: Jordy Kingama
// Date: 27/1/2020

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;

using tvshows.Models;

using Xamarin.Essentials;

namespace tvshows.Services
{
    public class FavoriteService
    {
        private readonly List<Show> shows;

        public FavoriteService()
        {
            string strCollection = Preferences.Get(nameof(shows), string.Empty);

            if (string.IsNullOrEmpty(strCollection))
            {
                shows = new List<Show>();
            }
            else
            {
                shows = JsonConvert.DeserializeObject<List<Show>>(strCollection);
            }
        }

        public void AddItem(Show show)
        {
            shows.Add(show);

            Save();
        }

        public void DeleteItem(Show show)
        {
            var deletedShow = shows.FirstOrDefault(s => s.Id == show.Id);
            int index = shows.IndexOf(deletedShow);
            shows.RemoveAt(index);

            Save();
        }

        public bool Exists(Show show)
        {
            if (show == null)
                return false;

            bool exists = false;

            foreach (var entry in shows)
            {
                if (entry.Id == show.Id)
                {
                    exists = true;
                    return exists;
                }
            }

            return exists;
        }

        private void Save()
        {
            string strCollection = JsonConvert.SerializeObject(shows);
            Preferences.Set(nameof(shows), strCollection);
        }

        public List<Show> GetShows()
        {
            return shows;
        }
    }
}
