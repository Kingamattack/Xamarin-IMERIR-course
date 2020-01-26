// File: DetailViewModel.cs
// Author: Jordy Kingama
// Date: 26/1/2020

using GalaSoft.MvvmLight;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Windows.Input;

using tvshows.Models;

using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

namespace tvshows.ViewModels
{
    public class DetailViewModel : ViewModelBase
    {
        public string Summary
        {
            get
            {
                if (show == null)
                    return string.Empty;

                if(show.Summary.Contains("<p>") || show.Summary.Contains("</p>") || show.Summary.Contains("<b>") || show.Summary.Contains("</b>"))
                {
                    var summary = show.Summary
                        .Replace("<p>", "")
                        .Replace("</p>", "")
                        .Replace("<b>", "")
                        .Replace("</b>", "");

                    return summary;
                }
                else
                {
                    return show.Summary;
                }
            }
        }

        private Show show;
        public Show Show
        {
            get => show;
            set
            {
                Set(ref show, value);
                RaisePropertyChanged(nameof(Summary));
                RaisePropertyChanged(nameof(ButtonText));
            }
        }

        public string ButtonText
        {
            get
            {
                if (show == null)
                    return "Ajouter aux favoris";

                return favoriteService.Exists(show) ? "Retirer des favoris" : "Ajouter aux favoris";
            }
        }

        public ICommand SaveToCollectionCommand => new Command(AddOrRemoveToCollection);

        private readonly FavoriteService favoriteService;

        public DetailViewModel()
        {
            favoriteService = new FavoriteService();
        }
        
        private void AddOrRemoveToCollection()
        {
            if(favoriteService.Exists(show))
            {
                favoriteService.DeleteItem(show);
            }
            else
            {
                favoriteService.AddItem(show);
            }

            RaisePropertyChanged(nameof(ButtonText));
        }
    }

    public class FavoriteService
    {
        private readonly List<Show> shows;

        public FavoriteService()
        {
            string strCollection = Preferences.Get(nameof(shows), string.Empty);

            if(string.IsNullOrEmpty(strCollection))
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
                if(entry.Id == show.Id)
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
    }
}