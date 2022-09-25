using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Java.Interop;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using Xamarin.Forms;

[assembly: Dependency(typeof(TravelRecordApp.Droid.Dependencies.Firestore))]
namespace TravelRecordApp.Droid.Dependencies
{
    class Firestore : Java.Lang.Object, IFirestore, IOnCompleteListener
    {
        List<Post> posts;
        bool hasReadPosts = false;
        public Firestore()
        {
            posts = new List<Post>();
        }
        
        public async Task<bool> Delete(Post post)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
               // collection.Document(post.Id).Delete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool Insert(Post post)
        {
            try { 
            var postDocument = new Dictionary<string, Java.Lang.Object>
            {
                {"experience", post.Experience },
                {"venuename", post.VenueName },
                {"catagoryId", post.CatagoryId },
                {"catagoryName", post.CatagoryName },
                {"address", post.Address },
                {"latitude", post.Latitude },
                {"longitude", post.Longitude },
                {"distance", post.Distance },
                {"userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid  },
            };
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
            collection.Add(new HashMap(postDocument));

            return true;
            }
            catch(Exception ex)
            {
               string abc=  ex.Message;
                return false;
            }
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if(task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;
                posts.Clear();
                foreach(var doc in documents.Documents)
                {
                    Post newPost = new Post() {
                        Experience = doc.Get("experience").ToString(),
                        VenueName = doc.Get("venuename").ToString(),
                        CatagoryId = doc.Get("catagoryId").ToString(),
                        CatagoryName = doc.Get("catagoryName").ToString(),
                        Address = doc.Get("Address").ToString(),
                        Latitude = (double)doc.Get("Latitude"),
                        Longitude = (double)doc.Get("longitude"),
                        Distance = (int)doc.Get("distance"),
                        //UserId = doc.Get("userId").ToString(),
                    };

                    posts.Add(newPost);
                }
            }
            else
            {
                posts.Clear();
            }
            hasReadPosts = true;
        }

        public async Task<List<Post>> Read()
        {
            try
            {


                hasReadPosts = false;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                var query = collection.WhereEqualTo("userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                query.Get().AddOnCompleteListener(this);

                for (int i = 0; i < 30; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    if (hasReadPosts)
                        break;
                }

                return posts;
            }

            catch(Exception ex)
            {
                return posts;
            }
        }

        
        public async Task<bool> Update(Post post)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>
            {
                {"experience", post.Experience },
                {"venuename", post.VenueName },
                {"catagoryId", post.CatagoryId },
                {"catagoryName", post.CatagoryName },
                {"address", post.Address },
                {"latitude", post.Latitude },
                {"longitude", post.Longitude },
                {"distance", post.Distance },
                {"userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid  },
            };
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
              //  collection.Document(post.Id).Update(postDocument);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}