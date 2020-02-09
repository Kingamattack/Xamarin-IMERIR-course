// File: ShowItemViewModel.cs
// Author: jordy
// Date: 9/2/2020

using GalaSoft.MvvmLight;

namespace tvshows.ViewModels.Items
{
    public class ShowItemViewModel : ViewModelBase
    {
        public bool IsFavorite { get; set; }

        public ShowItemViewModel()
        {
        }
    }
}
