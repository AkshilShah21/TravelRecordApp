using Plugin.Geolocator;
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
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            var venues = await Result.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
         }
        public void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var seletedVenue = venueListView.SelectedItem as Result;
                var firstCategory = seletedVenue.categories.FirstOrDefault();
                Post newPost = new Post()
                {
                    Experience = experienceEntry.Text,
                    CatagoryId = firstCategory.id,
                    CatagoryName = firstCategory.name,
                    Address = seletedVenue.location.address,
                    Distance = seletedVenue.distance,
                    Latitude = seletedVenue.geocodes.main.latitude,
                    Longitude = seletedVenue.geocodes.main.longitude,
                    VenueName = seletedVenue.name
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Post>();
                    int rows = conn.Insert(newPost);
                    if (rows > 0)
                    {
                        experienceEntry.Text = string.Empty;
                        DisplayAlert("success", "Experience succesfully inserted", "Ok");
                        
                    }
                    else
                        DisplayAlert("failure", "Experience failed to insert", "Ok");
                }
                /*bool result = Firestore.Insert(newPost);
                if (result) { 
                    experienceEntry.Text = string.Empty;
                    DisplayAlert("success", "Experience succesfully inserted", "Ok");
                    
                    Navigation.PopAsync();
                }
                else
                    DisplayAlert("failure", "Experience failed to insert", "Ok");*/
            }
            catch(NullReferenceException nre)
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}