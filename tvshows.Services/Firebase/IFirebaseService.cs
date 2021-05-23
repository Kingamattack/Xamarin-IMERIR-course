using tvshows.Models;

namespace tvshows.Services
{
    public interface IFirebaseService
    {
        void Save(BaseShow show);

        void Get();
    }
}