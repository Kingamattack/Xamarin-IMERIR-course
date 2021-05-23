// File: FavoriteService.cs
// Author: Jordy Kingama
// Date: 27/1/2020

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;

using tvshows.Models;
using tvshows.Services;

using Xamarin.Essentials;

namespace tvBaseShows.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly List<BaseShow> baseShows;

        public FavoriteService()
        {
            string strCollection = Preferences.Get(nameof(baseShows), string.Empty);

            if (string.IsNullOrEmpty(strCollection))
            {
                baseShows = new List<BaseShow>();
            }
            else
            {
                baseShows = JsonConvert.DeserializeObject<List<BaseShow>>(strCollection);
            }
        }

        public void AddItem(BaseShow BaseShow)
        {
            baseShows.Add(BaseShow);

            Save();
        }

        public void DeleteItem(BaseShow BaseShow)
        {
            var deletedBaseShow = baseShows.FirstOrDefault(s => s.Id == BaseShow.Id);
            int index = baseShows.IndexOf(deletedBaseShow);
            baseShows.RemoveAt(index);

            Save();
        }

        public bool Exists(BaseShow BaseShow)
        {
            if (BaseShow == null)
                return false;

            bool exists = false;

            foreach (var entry in baseShows)
            {
                if (entry.Id == BaseShow.Id)
                {
                    exists = true;
                    return exists;
                }
            }

            return exists;
        }

        private void Save()
        {
            string strCollection = JsonConvert.SerializeObject(baseShows);
            Preferences.Set(nameof(baseShows), strCollection);
        }

        public List<BaseShow> GetShows()
        {
            return baseShows;
        }
    }
}
