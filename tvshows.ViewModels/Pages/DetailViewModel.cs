// File: DetailViewModel.cs
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
using tvshows.Models.Entities;
using tvshows.Services;
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

        public ImageSource ToolbarItemIcon
        {
            get
            {
                if (Show == null || !favoriteService.Exists(Show))
                    return ImageSource.FromFile("ic_star");

                return ImageSource.FromFile("ic_star_filled");
            }
        }

        public int NumberEpisodes => Episodes?.Count ?? 0;

        public CastingViewModel CastingViewModel { get; set; }
        public EpisodesViewModel EpisodesViewModel { get; set; }

        public List<Actor> Actors => show?.Embedded?.Actors;
        public List<Episode> Episodes => show?.Embedded?.Episodes;

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
                    EpisodesViewModel.Episodes = Episodes;
                }
            }
        }

        public ICommand AppearingCommand { get; private set; }
        public ICommand OpenWebsiteCommand { get; private set; }
        public ICommand SaveToCollectionCommand { get; private set; }

        private readonly IShowService showService;
        private readonly IFavoriteService favoriteService;
        private readonly INavigationService navigationService;

        public DetailViewModel()
        {
            OpenWebsiteCommand = new Command(OpenWebsite);
            SaveToCollectionCommand = new Command(AddOrRemoveToCollection);
            AppearingCommand = new Command(async () => await Appearing());

            showService = SimpleIoc.Default.GetInstance<IShowService>();
            favoriteService = SimpleIoc.Default.GetInstance<IFavoriteService>();
            navigationService = SimpleIoc.Default.GetInstance<INavigationService>();

            CastingViewModel = new CastingViewModel();
            EpisodesViewModel = new EpisodesViewModel();
        }

        private async Task Appearing()
        {
            try
            {
                if(!favoriteService.Exists(show))
                {
                    Show = await showService.GetShow(show.Id);
                }

                RaisePropertyChanged(nameof(ToolbarItemIcon));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void OpenWebsite()
        {
            if(!string.IsNullOrEmpty(Show.OfficialSite))
            {
                navigationService.NavigateTo("Website", Show.OfficialSite, true);
            }
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