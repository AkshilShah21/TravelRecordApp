using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelRecordApp.Helpers
{
    public interface IAuth
    {
        Task<bool> RegisterUser(String email, string passwaord);
        Task<bool> LoginUser(String email, string passwaord);
        bool IsAuhtenticated();
        string GetCurrentUserId();
    }
    class Auth
    {
        private static IAuth auth = DependencyService.Get<IAuth>();
        public Auth()
        {

        }
        public static async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                return await auth.RegisterUser(email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "ok");
                return false;
            }
        }
        public static async Task<bool> LoginUser(string email, string password)
        {
            //if user doesnt exists then register
            try
            {
                return await auth.LoginUser(email, password);
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "ok");
                string resultMessage = "There is no user record corresponding to this identifier";
                if (ex.Message.Contains(resultMessage))
                     await App.Current.MainPage.DisplayAlert("not found",resultMessage, "ok");
                return false;
            }

        }
        public static bool IsAuhtenticated()
        {
             return auth.IsAuhtenticated();
        }
        public static string GetCurrentUserId()
        {
            return auth.GetCurrentUserId();
        }
    }
}
