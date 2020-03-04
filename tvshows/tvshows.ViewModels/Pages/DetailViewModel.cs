﻿// File: DetailViewModel.cs
// Author: Jordy Kingama
// Date: 26/1/2020

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using tvshows.Models;
using tvshows.Services;
using tvshows.Services.Navigation;
using tvshows.ViewModels.Views;

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

        public Color StatusColor
        {
            get
            {
                if (Show?.Status == "Ended")
                    return Color.Red;

                return Color.Green;
            }
        }

        public CastingViewModel CastingViewModel { get; set; }

        public List<Actor> Actors => show?.Embedded?.Actors;

        private Show show;
        public Show Show
        {
            get => show;
            set
            {
                Set(ref show, value);

                if (show != null)
                {
                    RefreshProperties();

                    CastingViewModel.Actors = Actors;
                }
            }
        }

        public ICommand AppearingCommand { get; private set; }
        public ICommand OpenWebsiteCommand { get; private set; }
        public ICommand SaveToCollectionCommand { get; private set; }

        private readonly IShowService showService;
        private readonly IFavoriteService favoriteService;
        private readonly INavigationService2 navigationService;

        public DetailViewModel()
        {
            OpenWebsiteCommand = new Command(OpenWebsite);
            SaveToCollectionCommand = new Command(AddOrRemoveToCollection);
            AppearingCommand = new Command(async () => await Appearing());

            showService = SimpleIoc.Default.GetInstance<IShowService>();
            favoriteService = SimpleIoc.Default.GetInstance<IFavoriteService>();
            navigationService = SimpleIoc.Default.GetInstance<INavigationService2>();

            CastingViewModel = new CastingViewModel();
        }

        private async Task Appearing()
        {
            try
            {
                Show = await showService.GetShow(show.Id);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }            
        }

        private void OpenWebsite()
        {
            navigationService.NavigateTo("Website", Show.Url, true);
        }

        private void AddOrRemoveToCollection()
        {
            if (favoriteService.Exists(show))
            {
                favoriteService.DeleteItem(show);
            }
            else
            {
                favoriteService.AddItem(show);
            }

            //RaisePropertyChanged(nameof(ButtonText));
        }

        private void RefreshProperties()
        {
            RaisePropertyChanged(nameof(Summary));
            RaisePropertyChanged(nameof(StatusColor));
            RaisePropertyChanged(nameof(Actors));
        }
    }    
}