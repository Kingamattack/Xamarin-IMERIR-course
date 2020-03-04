// File: Actor.cs
// Author: Jordy Kingama
// Date: 3/3/2020

using Newtonsoft.Json;

using tvshows.Models.Entities;

namespace tvshows.Models
{
    public class Actor
    {
        public Person Person { get; set; }

        public Character Character { get; set; }
    }

    public class Character
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }
    }

    public class Person
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}