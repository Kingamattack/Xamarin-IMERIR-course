using System;
using Firebase.Database;
using Foundation;
using tvshows.Models;
using tvshows.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(tvshows.iOS.FirebaseService))]
namespace tvshows.iOS
{
    public class FirebaseService: IFirebaseService
    {
        private readonly DatabaseReference databaseReference;

        public FirebaseService()
        {
            databaseReference = Database.DefaultInstance.GetRootReference();
        }

        public T Get<T>()
        {
            throw new NotImplementedException();
        }

        public void Save(Show show)
        {
            var dictionnary = GetDictionnaryFromObject(show);

            databaseReference.GetChild("shows").GetChildByAutoId().SetValue(dictionnary);
        }

        private NSMutableDictionary<NSObject, NSString> GetDictionnaryFromObject(Show show)
        {
            var nsDictionnary = new NSMutableDictionary<NSObject, NSString>
            {
                { (NSString)nameof(show.Id), (NSNumber)show.Id  },
                { (NSString)nameof(show.Name), (NSString)show.Name },
                { (NSString)nameof(show.Image), (NSString)show.Image.Original }
            };

            return nsDictionnary;
        }
    }
}
