// File: DetailViewModel.cs
// Author: Jordy Kingama
// Date: 26/1/2020

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

using tvshows.Models;
using tvshows.Models.Entities;
using tvshows.Services;
using tvshows.Services.Navigation;
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

        public List<Actor> Actors => show?.Embedded?.Actors;

        public List<Season> Seasons => show?.Embedded?.Seasons;

        private Show show;
        public Show Show
        {
            get => show;
            set
            {
                Set(ref show, value);
                RaisePropertyChanged(nameof(Summary));
                RaisePropertyChanged(nameof(StatusColor));
                RaisePropertyChanged(nameof(Actors));
                RaisePropertyChanged(nameof(Seasons));
            }
        }

        public ICommand OpenWebsiteCommand { get; private set; }
        public ICommand SaveToCollectionCommand { get; private set; }
        public ICommand AppearingCommand { get; private set; }

        private readonly IFavoriteService favoriteService;
        private readonly INavigationService2 navigationService;
        private readonly IShowService showService;

        public DetailViewModel()
        {
            OpenWebsiteCommand = new Command(OpenWebsite);
            //SaveToCollectionCommand = new Command(AddOrRemoveToCollection);
            AppearingCommand = new Command(async () => await Appearing());
            favoriteService = SimpleIoc.Default.GetInstance<IFavoriteService>();
            navigationService = SimpleIoc.Default.GetInstance<INavigationService2>();
            showService = SimpleIoc.Default.GetInstance<IShowService>();

        }

        private async Task Appearing()
        {
            try
            {
                Show = await showService.GetShow(show.Id);
            }
            catch (Exception ex)
            {

            }            
        }

        private void OpenWebsite()
        {
            navigationService.NavigateTo("Website", Show.Url, true);
        }

        //private void AddOrRemoveToCollection()
        //{
        //    if(favoriteService.Exists(show))
        //    {
        //        favoriteService.DeleteItem(show);
        //    }
        //    else
        //    {
        //        favoriteService.AddItem(show);
        //    }

        //    RaisePropertyChanged(nameof(ButtonText));
        //}
    }    
}