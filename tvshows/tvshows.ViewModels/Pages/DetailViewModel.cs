﻿// File: DetailViewModel.cs
// Author: Jordy Kingama
// Date: 26/1/2020

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private readonly IFavoriteService favoriteService;

        public DetailViewModel()
        {
            favoriteService = SimpleIoc.Default.GetInstance<IFavoriteService>();
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