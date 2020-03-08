// File: CastingViewModel.cs
// Author: Jordy Kingama
// Date: 4/3/2020

using GalaSoft.MvvmLight;

using System.Collections.Generic;

using tvshows.Models;

namespace tvshows.ViewModels.Views
{
    public class CastingViewModel : ViewModelBase
    {
        private List<Actor> actors;
        public List<Actor> Actors
        {
            get => actors;
            set => Set(ref actors, value);
        }
    }
}