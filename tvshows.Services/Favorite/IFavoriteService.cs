// File: IFavoriteService.cs
// Author: jordy
// Date: 8/2/2020

using System.Collections.Generic;

using tvshows.Models;

namespace tvshows.Services
{
    public interface IFavoriteService
    {
        void AddItem(BaseShow show);

        void DeleteItem(BaseShow show);

        bool Exists(BaseShow show);

        List<BaseShow> GetShows();
    }
}