// File: IShowService.cs
// Author: Jordy Kingama
// Date: 3/3/2020

using System.Collections.Generic;
using System.Threading.Tasks;

using tvshows.Models;

namespace tvshows.Services
{
    public interface IShowService
    {
        Task<List<Show>> GetShows(string query);

        Task<Show> GetShow(int id);
    }
}