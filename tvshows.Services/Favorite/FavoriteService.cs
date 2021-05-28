// File: FavoriteService.cs
// Author: Jordy Kingama
// Date: 27/1/2020

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;

using tvshows.Services;

using Xamarin.Essentials;

namespace tvBaseShows.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly List<int> shows;

        public FavoriteService()
        {
            shows = new List<int>();

            string strCollection = Preferences.Get(nameof(shows), string.Empty);

            if (!string.IsNullOrEmpty(strCollection))
            {
                shows = JsonConvert
                    .DeserializeObject<List<int>>(strCollection)
                    .Select(id => id)
                    .ToList();
            }
        }

        public void AddItem(int id)
        {
            shows.Add(id);
            Save();
        }

        public void DeleteItem(int id)
        {
            var deletedShow = shows.FirstOrDefault(s => s == id);
            int index = shows.IndexOf(deletedShow);
            shows.RemoveAt(index);

            Save();
        }

        public bool Exists(int id)
        {
            return shows.Any(myId => myId == id);
        }

        public List<int> GetShowIds()
        {
            return shows;
        }

        private void Save()
        {
            string strCollection = JsonConvert.SerializeObject(shows);
            Preferences.Set(nameof(shows), strCollection);
        }        
    }
}
