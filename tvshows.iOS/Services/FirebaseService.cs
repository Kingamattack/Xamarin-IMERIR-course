// File: FirebaseService.cs
// Author: Jordy Kingama
// Date: 23/05/2021

using System.Collections.Generic;
using Firebase.Database;

using Foundation;

using GalaSoft.MvvmLight.Ioc;

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
        private readonly IFavoriteService favoriteService;
        private readonly DatabaseReference databaseReference;

        public FirebaseService()
        {
            favoriteService = SimpleIoc.Default.GetInstance<IFavoriteService>();
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
                    var shows = GetBaseShowFromDictionnary(showsDictionnary);

                    MessagingCenter.Send(shows, "GetShows");
                }
            });            
        }

        public void Save(BaseShow show)
        {
            var dictionnary = GetDictionnaryFromObject(show);
            showRefence.GetChildByAutoId().SetValue(dictionnary);
            // favoriteService.AddItem(show);
        }

        private NSMutableDictionary<NSString, NSObject> GetDictionnaryFromObject(BaseShow show)
        {
            var nsDictionnary = new NSMutableDictionary<NSString, NSObject>
            {
                { (NSString)nameof(show.Id), (NSNumber)show.Id  },
                { (NSString)nameof(show.Name), (NSString)show.Name },
                { (NSString)nameof(show.Image), (NSString)show.Image }
            };

            return nsDictionnary;
        }

        private List<BaseShow> GetBaseShowFromDictionnary(NSDictionary dictionnary)
        {
            var BaseShows = new List<BaseShow>();

            foreach (var entry in dictionnary)
            {
                var dict = entry.Value as NSDictionary;

                var show = new BaseShow
                {
                    Id = ((NSNumber)dict["Id"]).Int32Value,
                    Name = (NSString)dict["Name"],
                    Image = (NSString)dict["Image"]
                };

                BaseShows.Add(show);
            }

            return BaseShows;
        }
    }
}