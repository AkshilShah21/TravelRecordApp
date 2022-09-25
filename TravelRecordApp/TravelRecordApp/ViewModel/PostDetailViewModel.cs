using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp.ViewModel
{
    class PostDetailViewModel
    {
        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Post SelectedPost { get; set; }
        public PostDetailViewModel()
        {
            UpdateCommand = new Command<string>(Update, CanUpdate);
            DeleteCommand = new Command(Delete);
        }
        private async void Update(string newExperience)
        {

            SelectedPost.Experience = newExperience;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                try
                {
                    int rows = conn.Update(SelectedPost);

                    if (rows > 0)
                    {
                        await App.Current.MainPage.DisplayAlert("success", "Experience succesfully udpdated", "Ok");
                        await App.Current.MainPage.Navigation.PopAsync();
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("failure", "Experience failed to updated ", "Ok");
                    /*bool result = await Firestore.Update(_selectPost);
                    if (result)
                        await Navigation.PopAsync();*/
                }
                catch (Execption e)
                {

                }
            }
        }
        private bool CanUpdate(string newExperience)
        {
            if (string.IsNullOrWhiteSpace(newExperience))
                return false;
            return true;
        }

        private async void Delete()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Post>();
                    int rows = conn.Delete(SelectedPost);
                    if (rows > 0)
                    {
                        await App.Current.MainPage.DisplayAlert("success", "Experience succesfully deleted", "Ok");
                        await App.Current.MainPage.Navigation.PopAsync();
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("failure", "Experience failure to be deleted", "Ok");
                    /* bool result = await Firestore.Delete(_selectPost);
                     if (result)
                         await Navigation.PopAsync();*/
                }
            }
            catch (Execption e)
            {

            }
        }
    }

}
