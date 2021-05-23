// File: FirebaseService.cs
// Author: Jordy Kingama
// Date: 23/05/2021

using System.Collections.Generic;
using Firebase.Database;

using Foundation;

using tvshows.iOS;
using tvshows.Models;
using tvshows.Services;

using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseService))]
namespace tvshows.iOS
{
    public class FirebaseService : IFirebaseService
    {
        private readonly DatabaseReference showRefence;
        private readonly DatabaseReference databaseReference;

        public FirebaseService()
        {
            databaseReference = Database.DefaultInstance.GetRootReference();
            showRefence = databaseReference.GetChild("shows");
        }

        public void Get()
        {
            showRefence.ObserveEvent(DataEventType.Value, (snapshot) =>
            {
                if (snapshot.Exists)
                {
                    var showsDictionnary = snapshot.GetValue() as NSDictionary;
                    var shows = GetShowFavoriteFromDictionnary(showsDictionnary);

                    MessagingCenter.Send(shows, "GetShows");
                }
            });            
        }

        public void Save(ShowFavorite show)
        {
            var dictionnary = GetDictionnaryFromObject(show);
            showRefence.GetChildByAutoId().SetValue(dictionnary);
        }

        private NSMutableDictionary<NSString, NSObject> GetDictionnaryFromObject(ShowFavorite show)
        {
            var nsDictionnary = new NSMutableDictionary<NSString, NSObject>
            {
                { (NSString)nameof(show.Id), (NSNumber)show.Id  },
                { (NSString)nameof(show.Name), (NSString)show.Name },
                { (NSString)nameof(show.Image), (NSString)show.Image }
            };

            return nsDictionnary;
        }

        private List<ShowFavorite> GetShowFavoriteFromDictionnary(NSDictionary dictionnary)
        {
            var showFavorites = new List<ShowFavorite>();

            foreach (var entry in dictionnary)
            {
                var dict = entry.Value as NSDictionary;

                var show = new ShowFavorite
                {
                    Id = ((NSNumber)dict["Id"]).Int32Value,
                    Name = (NSString)dict["Name"],
                    Image = (NSString)dict["Image"]
                };

                showFavorites.Add(show);
            }

            return showFavorites;
        }
    }
}