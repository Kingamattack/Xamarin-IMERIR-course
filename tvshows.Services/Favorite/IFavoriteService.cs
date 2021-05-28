// File: IFavoriteService.cs
// Author: jordy
// Date: 8/2/2020

using System.Collections.Generic;

namespace tvshows.Services
{
    public interface IFavoriteService
    {
        void AddItem(int id);

        void DeleteItem(int id);

        bool Exists(int id);

        List<int> GetShowIds();
    }
}