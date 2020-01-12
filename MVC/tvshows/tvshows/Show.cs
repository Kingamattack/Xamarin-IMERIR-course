﻿// File: Show.cs
// Author: Jordy Kingama
// Date: 12/1/2020

using System.Collections.Generic;

namespace tvshows
{
    public class Show
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public List<string> Genres { get; set; }
    }

    public class JsonShow
    {
        public double Score { get; set; }
        public Show Show { get; set; }
    }
}