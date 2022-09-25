using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TravelRecordApp.Model;
using System.Linq;

namespace TravelRecordApp.ViewModel
{
    class ProfileViewModel
    {
        public ObservableCollection<CategoryCount> Categories { get; set; }
        
        public ProfileViewModel()
        {
            Categories = new ObservableCollection<CategoryCount>();
        }
        public void GetPosts()
        {
            Categories.Clear();
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();
            
                var categories = (from p in posts
                                  orderby p.CatagoryId
                                  select p.CatagoryName).Distinct().ToList();
                foreach (var category in categories)
                {
                    var count = (from post in posts
                                 where post.CatagoryName == category
                                 select post).ToList().Count;

                    Categories.Add(new CategoryCount
                    {
                        Name = category,
                        Count = count
                    });
                }
            
        }

       public class CategoryCount
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }
    }
}
