// File: Actor.cs
// Author: Jordy Kingama
// Date: 3/3/2020

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
        public long Id { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
    }

    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}