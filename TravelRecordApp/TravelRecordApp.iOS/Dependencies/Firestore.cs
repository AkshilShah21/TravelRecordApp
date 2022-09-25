using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(TravelRecordApp.iOS.Dependencies.Firestore))]
namespace TravelRecordApp.iOS.Dependencies
{
    class Firestore : IFirestore
    {
        public async Task<bool> Delete(Post post)
        {
            try { 
            var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
           // await  collection.GetDocument(post.Id).DeleteDocumentAsync();
            return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Insert(Post post)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("experience"),
                    new NSString("venuename"),
                    new NSString("catagoryId"),
                    new NSString("catagoryName"),
                    new NSString("address"),
                    new NSString("latitude"),
                    new NSString("longitude"),
                    new NSString("distance"),
                    new NSString("userId")
                };
                var values = new NSObject[]
                {
                    new NSString(post.Experience),
                    new NSString(post.VenueName),
                    new NSString(post.CatagoryId),
                    new NSString(post.CatagoryName),
                    new NSString(post.Address),
                    new NSNumber(post.Latitude),
                    new NSNumber(post.Longitude),
                    new NSNumber(post.Distance),
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid),

                };

                var document = new NSDictionary<NSString, NSObject>(keys, values);

                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
                collection.AddDocument(document);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<Post>> Read()
        {
            try { 
            var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
            var query = collection.WhereEqualsTo("userId", Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid);
            var documents = await query.GetDocumentsAsync();

            List<Post> posts = new List<Post>();
            foreach(var doc in documents.Documents)
            {
               var dictonary = doc.Data;
                var newPost = new Post()
                {
                    Experience = dictonary.ValueForKey(new NSString("experience")) as NSString,
                    VenueName = dictonary.ValueForKey(new NSString("venuename")) as NSString,
                    CatagoryId = dictonary.ValueForKey(new NSString("catagoryId")) as NSString,
                    CatagoryName = dictonary.ValueForKey(new NSString("catagoryName")) as NSString,
                    Address = dictonary.ValueForKey(new NSString("address")) as NSString,
                    Latitude = (double)(dictonary.ValueForKey(new NSString("Latitude")) as NSNumber),
                    Longitude = (double)(dictonary.ValueForKey(new NSString("Longitude")) as NSNumber),
                    Distance = (int)(dictonary.ValueForKey(new NSString("distance")) as NSNumber),
                    //UserId = dictonary.ValueForKey(new NSString("userId")) as NSString,
                   // Id = doc.Id
                };
                posts.Add(newPost);
            }
            return posts;
            }
            catch(Exception ex)
            {
                return new List<Post>();
            }
        }

        public async Task<bool> Update(Post post)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("experience"),
                    new NSString("venuename"),
                    new NSString("catagoryId"),
                    new NSString("catagoryName"),
                    new NSString("address"),
                    new NSString("latitude"),
                    new NSString("longitude"),
                    new NSString("distance"),
                    new NSString("userId")
                };
                var values = new NSObject[]
                {
                    new NSString(post.Experience),
                    new NSString(post.VenueName),
                    new NSString(post.CatagoryId),
                    new NSString(post.CatagoryName),
                    new NSString(post.Address),
                    new NSNumber(post.Latitude),
                    new NSNumber(post.Longitude),
                    new NSNumber(post.Distance),
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid),

                };

                var document = new NSDictionary<NSObject, NSObject>(keys, values);

                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
               // await collection.GetDocument(post.Id).UpdateDataAsync(document);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}