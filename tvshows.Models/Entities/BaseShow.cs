using System.Collections.Generic;

namespace tvshows.Models
{
    public class BaseShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string>? Genres { get; set; }
        public string Image { get; set; }
        public string? Runtime { get; set; }
    }
}