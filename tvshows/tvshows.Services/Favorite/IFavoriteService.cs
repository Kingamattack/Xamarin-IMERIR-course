// File: IFavoriteService.cs
// Author: jordy
// Date: 8/2/2020

using System.Collections.Generic;

using tvshows.Models;

namespace tvshows.Services
{
    public interface IFavoriteService
    {
        void AddItem(Show show);

        void DeleteItem(Show show);

        bool Exists(Show show);

        List<Show> GetShows();
    }
}