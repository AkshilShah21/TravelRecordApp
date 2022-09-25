using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisteredPage : ContentPage
    {
        public RegisteredPage()
        {
            InitializeComponent();
        }

        
        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isFullNameEmpty = string.IsNullOrEmpty(fullNameEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
            bool isPassword2Empty = string.IsNullOrEmpty(passwordEntry2.Text);
            if (isEmailEmpty || isFullNameEmpty || isPasswordEmpty || isPassword2Empty)
            {
                await App.Current.MainPage.DisplayAlert("Invalid", "Field canot be empty", "try again");
            }
            else
            {
                if(passwordEntry.Text != passwordEntry2.Text)
                {
                    await App.Current.MainPage.DisplayAlert("Invalid", "password doesnot matchs", "try again");
                }
                else
                {
                   await Auth.RegisterUser(emailEntry.Text, passwordEntry.Text);
                    
                }
            }
            await App.Current.MainPage.DisplayAlert("Success", "user Registered", "ok");
            await Navigation.PushAsync(new MainPage());
        }
    }
}