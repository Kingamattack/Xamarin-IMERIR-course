// File: DetailViewModel.cs
// Author: Jordy Kingama
// Date: 26/1/2020

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System.Collections.Generic;
using System.Windows.Input;

using tvshows.Models;
using tvshows.Services;

using Xamarin.Forms;

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

        public List<string> Genres => Show?.Genres;

        public Color StatusColor
        {
            get
            {
                if (Show?.Status == "Ended")
                    return Color.Red;

                return Color.Green;
            }
        }

        //public double Note => Show?.Rating.Average ?? 0.0;

        private Show show;
        public Show Show
        {
            get => show;
            set
            {
                Set(ref show, value);
                RaisePropertyChanged(nameof(Summary));
                RaisePropertyChanged(nameof(ButtonText));
                RaisePropertyChanged(nameof(StatusColor));
                RaisePropertyChanged(nameof(Genres));
                //RaisePropertyChanged(nameof(Note));
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

        public ICommand OpenWebsiteCommand { get; private set; }
        public ICommand SaveToCollectionCommand { get; private set; }

        private readonly IFavoriteService favoriteService;
        private readonly INavigationService navigationService;

        public DetailViewModel()
        {
            OpenWebsiteCommand = new Command(OpenWebsite);
            SaveToCollectionCommand = new Command(AddOrRemoveToCollection);
            favoriteService = SimpleIoc.Default.GetInstance<IFavoriteService>();
            navigationService = SimpleIoc.Default.GetInstance<INavigationService>();

        }

        private void OpenWebsite()
        {
            navigationService.NavigateTo("Website");
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
}