using GalaSoft.MvvmLight;

namespace tvshows.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => Set(ref isBusy, value);
        }
    }
}
