// File: Episode.cs
// Author: Jordy Kingama
// Date: 4/3/2020

namespace tvshows.Models.Entities
{
    public class Episode
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public string Airdate { get; set; }
        public int Runtime { get; set; }
        public Image Image { get; set; }
        public string Summary { get; set; }
    }
}