﻿// File: Show.cs
// Author: Jordy Kingama
// Date: 12/1/2020

using Newtonsoft.Json;

using System.Collections.Generic;

using tvshows.Models.Entities;

namespace tvshows.Models
{
    public class Show : BaseShow
    {
        public string OfficialSite { get; set; }
        public string Summary { get; set; }
        public Rating Rating { get; set; }
        public string Status { get; set; }
        public string Premiered { get; set; }
        public Image Image { get; set; }

        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    public class Rating
    {
        public double? Average { get; set; }
    }

    public class Embedded
    {
        [JsonProperty("cast")]
        public List<Actor> Actors { get; set; }

        [JsonProperty("episodes")]
        public List<Episode> Episodes { get; set; }
    }    
}