// File: EpisodesViewModel.cs
// Author: Jordy Kingama
// Date: 4/3/2020

using GalaSoft.MvvmLight;

using System.Collections.ObjectModel;

using tvshows.Models.Entities;

namespace tvshows.ViewModels.Views
{
    public class EpisodesViewModel : ViewModelBase
    {
        private ObservableCollection<Episode> episodes;
        public ObservableCollection<Episode> Episodes
        {
            get => episodes;
            set => Set(ref episodes, value);
        }
    }
}