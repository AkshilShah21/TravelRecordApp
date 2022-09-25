using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                var postTable = conn.Table<Post>().ToList();
                /*
                            var postTable = await Firestore.Read();*/
                var categories = (from p in postTable
                                  orderby p.CatagoryId
                                  select p.CatagoryName).Distinct().ToList();

                Dictionary<string, int> categoriesCount = new Dictionary<string, int>();
                foreach (var category in categories)
                {
                    var count = (from post in postTable
                                 where post.CatagoryName == category
                                 select post).ToList().Count;
                    categoriesCount.Add(category, count);
                }
                categoriesListView.ItemsSource = categoriesCount;
                postCountLabel.Text = postTable.Count.ToString();
                // }
            }
        }
    }
}
