﻿// File: DetailViewModel.cs
// Author: Jordy Kingama
// Date: 26/1/2020

using GalaSoft.MvvmLight.Ioc;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using tvshows.Models;
using tvshows.Models.Entities;
using tvshows.Services;
using tvshows.Strings;
using tvshows.ViewModels.Views;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace tvshows.ViewModels
{
    [QueryProperty(nameof(SelectedShowId), "show")]
    public class DetailViewModel : BaseViewModel
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

        public ImageSource ToolbarItemIcon
        {
            get
            {
                if (Show == null || !favoriteService.Exists(Show.Id))
                    return ImageSource.FromFile("ic_star");

                return ImageSource.FromFile("ic_star_filled");
            }
        }

        public int NumberEpisodes => Episodes?.Count ?? 0;

        private CastingViewModel castingViewModel;
        public CastingViewModel CastingViewModel
        {
            get => castingViewModel;
            set => Set(ref castingViewModel, value);
        }

        private EpisodesViewModel episodesViewModel;
        public EpisodesViewModel EpisodesViewModel
        {
            get => episodesViewModel;
            set => Set(ref episodesViewModel, value);
        }

        public List<Actor> Actors => show?.Embedded?.Actors;
        public List<Episode> Episodes => show?.Embedded?.Episodes;

        public string SelectedShowId { get; set; }

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

                    if(Actors != null)
                    {
                        CastingViewModel = new CastingViewModel
                        {
                            Actors = new ObservableCollection<Actor>(Actors)
                        };
                    }

                    if (Episodes != null)
                    {
                        EpisodesViewModel = new EpisodesViewModel
                        {
                            Episodes = new ObservableCollection<Episode>(Episodes)
                        };
                    }
                }
            }
        }

        public ICommand AppearingCommand { get; private set; }
        public ICommand OpenWebsiteCommand { get; private set; }
        public ICommand SaveToCollectionCommand { get; private set; }

        private readonly IShowService showService;
        private readonly IFavoriteService favoriteService;
        private readonly IFirebaseService firebaseService;

        public DetailViewModel()
        {
            OpenWebsiteCommand = new Command(async () => await OpenWebsite());
            SaveToCollectionCommand = new Command(async () => await AddOrRemoveToCollection());
            AppearingCommand = new Command(async () => await Appearing());

            showService = SimpleIoc.Default.GetInstance<IShowService>();
            favoriteService = SimpleIoc.Default.GetInstance<IFavoriteService>();
            firebaseService = DependencyService.Get<IFirebaseService>();

            CastingViewModel = new CastingViewModel();
            EpisodesViewModel = new EpisodesViewModel();
        }

        private async Task Appearing()
        {
            try
            {
                Show = await showService.GetShow(int.Parse(SelectedShowId));

                RaisePropertyChanged(nameof(ToolbarItemIcon));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private async Task OpenWebsite()
        {
            if(!string.IsNullOrEmpty(Show.OfficialSite))
            {
                await Browser.OpenAsync(Show.OfficialSite);
            }
        }

        private async Task AddOrRemoveToCollection()
        {
            var baseShow = new BaseShow
            {
                Id = show.Id,
                Name = show.Name,
                Image = show.Image.Original
            };

            // firebaseService.Save(baseShow);


            if (favoriteService.Exists(baseShow.Id))
            {
                favoriteService.DeleteItem(baseShow.Id);
                await Shell.Current.DisplayAlert(Localization.DetailAlertDeleteTitle, Localization.DetailAlertDeleteMessage, Localization.DetailAlertOk);
            }
            else
            {
                favoriteService.AddItem(baseShow.Id);
                await Shell.Current.DisplayAlert(Localization.DetailAlertAddTitle, Localization.DetailAlertAddMessage, Localization.DetailAlertOk);
            }

            RaisePropertyChanged(nameof(ToolbarItemIcon));
        }

        private void RefreshProperties()
        {
            RaisePropertyChanged(nameof(Summary));
            RaisePropertyChanged(nameof(StatusColor));
            RaisePropertyChanged(nameof(Actors));
            RaisePropertyChanged(nameof(Episodes));
            RaisePropertyChanged(nameof(NumberEpisodes));
        }
    }    
}