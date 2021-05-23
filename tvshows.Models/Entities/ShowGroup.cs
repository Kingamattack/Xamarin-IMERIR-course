// File: ShowGroup.cs
// Author: Jordy Kingama
// Date: 4/3/2020

using System.Collections.Generic;

namespace tvshows.Models.Entities
{
    public class Showgroup : List<ShowFavorite>
    {
        public string Name { get; set; }

        public Showgroup(string name, List<ShowFavorite> shows) : base(shows)
        {
            Name = name;
        }
    }
}