// File: FirebaseService.cs
// Author: Jordy Kingama
// Date: 23/05/2021

using Firebase.Database;

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
}