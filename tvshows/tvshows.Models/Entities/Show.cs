// File: Show.cs
// Author: Jordy Kingama
// Date: 12/1/2020

using Newtonsoft.Json;

using System.Collections.Generic;

using tvshows.Models.Entities;

namespace tvshows.Models
{
    public class Show
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public int? Runtime { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public string Premiered { get; set; }
        public Image Image { get; set; }
        public List<string> Genres { get; set; }

        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    public class Embedded
    {
        [JsonProperty("cast")]
        public List<Actor> Actors { get; set; }

        [JsonProperty("seasons")]
        public List<Season> Seasons { get; set; }

        [JsonProperty("episodes")]
        public List<Episode> Episodes { get; set; }
    }    
}