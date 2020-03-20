// File: CastingViewModel.cs
// Author: Jordy Kingama
// Date: 4/3/2020

using GalaSoft.MvvmLight;

using System.Collections.ObjectModel;

using tvshows.Models;

namespace tvshows.ViewModels.Views
{
    public class CastingViewModel : ViewModelBase
    {
        private ObservableCollection<Actor> actors;
        public ObservableCollection<Actor> Actors
        {
            get => actors;
            set => Set(ref actors, value);
        }
    }
}