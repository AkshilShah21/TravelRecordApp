using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel
{
    class HistoryViewModel
    {
        public ObservableCollection<Post> Posts { get; set; }
        public HistoryViewModel()
        {
            Posts = new ObservableCollection<Post>();
        }
        public void GetPosts()
        {
            Posts.Clear();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();
                foreach(var post in posts)
                {
                    Posts.Add(post);
                }
            }
        }
    }
    
}
