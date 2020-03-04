// File: Season.cs
// Author: Jordy Kingama
// Date: 3/3/2020

using Newtonsoft.Json;

namespace tvshows.Models.Entities
{
    public class Season
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("episodeOrder")]
        public long? EpisodeOrder { get; set; }
    }
}