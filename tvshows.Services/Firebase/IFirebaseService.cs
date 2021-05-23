using tvshows.Models;

namespace tvshows.Services
{
    public interface IFirebaseService
    {
        void Save(ShowFavorite show);

        void Get();
    }
}