using tvshows.Models;

namespace tvshows.Services
{
    public interface IFirebaseService
    {
        void Save(Show data);

        T Get<T>();
    }
}