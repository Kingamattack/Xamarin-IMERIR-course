// File: SearchViewModel.cs
// Author: jordy
// Date: 20/1/2020

using GalaSoft.MvvmLight.Ioc;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using tvshows.Models;
using tvshows.Services;

using Xamarin.Forms;

namespace tvshows.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private string text;
        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        private ObservableCollection<BaseShow> shows;
        public ObservableCollection<BaseShow> Shows
        {
            get => shows;
            set => Set(ref shows, value);
        }

        private BaseShow selectedShow;
        public BaseShow SelectedShow
        {
            get => selectedShow;
            set
            {
                if(value != null)
                {
                    Set(ref selectedShow, value);
                    GoToDetailsCommand.Execute(selectedShow);
                }
            }
        }

        private readonly IShowService showService;

        public ICommand SearchCommand { get; private set; }
        public ICommand GoToDetailsCommand { get; private set; }

        public SearchViewModel()
        {
            IsBusy = false;
            Text = string.Empty;
            Shows = new ObservableCollection<BaseShow>();

            showService = SimpleIoc.Default.GetInstance<IShowService>();

            SearchCommand = new Command<string>(async (string query) => await Search(query));
            GoToDetailsCommand = new Command<BaseShow>(async (BaseShow show) => await GoToDetails(show));
        }

        private async Task Search(string query)
        {
            try
            {
                IsBusy = true;

                if (query?.Length >= 3)
                {
                    var shows = await showService.GetShows(query);
                    var BaseShowes = shows.Select(s => new BaseShow
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Genres = s.Genres,
                        Image = s.Image?.Original ?? "",
                        Runtime = s.Runtime,
                    });

                    Shows = new ObservableCollection<BaseShow>(BaseShowes);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{nameof(SearchViewModel)}: {e.StackTrace}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GoToDetails(BaseShow show)
        {
            await Shell.Current.GoToAsync($"DetailsPage?show={show.Id}", true);
        }
    }
}