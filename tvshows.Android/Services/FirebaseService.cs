// File: FirebaseService.cs
// Author: Jordy Kingama
// Date: 23/05/2021

using System.Collections.Generic;
using System.Linq;

using Firebase.Database;

using Java.Lang;
using Java.Util;

using tvshows.Droid;
using tvshows.Models;
using tvshows.Services;

using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseService))]
namespace tvshows.Droid
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseDatabase database;
        private readonly DatabaseReference showRefence;

        public FirebaseService()
        {
            database = FirebaseDatabase.Instance;
            showRefence = database.GetReference("shows");
        }

        public void Get()
        {
            showRefence.AddValueEventListener(new ValueEventListener());
        }

        public void Save(ShowFavorite show)
        {
            var dictionnary = GetDictionnaryFromObject(show);
            var reference = showRefence.Push();
            reference.SetValue(dictionnary);
        }

        private HashMap GetDictionnaryFromObject(ShowFavorite show)
        {
            var map = new HashMap();
            map.Put(nameof(show.Id), show.Id);
            map.Put(nameof(show.Name), show.Name);
            map.Put(nameof(show.Image), show.Image);

            return map;
        }
    }

    public class ValueEventListener : Object, IValueEventListener
    {
        public void OnCancelled(DatabaseError error) { }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Exists())
            {
                var shows = GetShowFavoriteFromDictionnary(snapshot);
                MessagingCenter.Send(shows, "GetShows");
            }
        }

        private List<ShowFavorite> GetShowFavoriteFromDictionnary(DataSnapshot snapshot)
        {
            var showFavorites = new List<ShowFavorite>();

            foreach (DataSnapshot snap in snapshot.Children.ToEnumerable())
            {
                var show = new ShowFavorite
                {
                    Id = (int)snap.Child("Id")?.GetValue(true),
                    Name = (string)snap.Child("Name")?.GetValue(true),
                    Image = (string)snap.Child("Image")?.GetValue(true)
                };

                showFavorites.Add(show);
            }

            return showFavorites;
        }
    }
}