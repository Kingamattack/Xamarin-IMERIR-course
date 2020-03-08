// File: ShowItemViewModel.cs
// Author: jordy
// Date: 9/2/2020

using GalaSoft.MvvmLight;
using tvshows.Models;

namespace tvshows.ViewModels.Items
{
    public class ShowItemViewModel : ViewModelBase
    {
        private Show show;
        public Show Show
        {
            get => show;
            set => Set(ref show, value);
        }

        public int NumberEpisode => show?.Genres.Count ?? 0;

        public ShowItemViewModel()
        {
        }
    }
}