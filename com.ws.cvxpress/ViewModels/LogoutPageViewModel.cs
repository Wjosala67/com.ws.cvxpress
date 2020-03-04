using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Views.Start;
using Plugin.FacebookClient;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class LogoutPageViewModel
    {
        public Command OnLogoutCommand { get; set; }

        public LogoutPageViewModel()
        {
            
            OnLogoutCommand = new Command(() =>
            {
                if (CrossFacebookClient.Current.IsLoggedIn)
                {
                    CrossFacebookClient.Current.Logout();

                }


                Profile profile = new Profile();
                DatabaseHelper.Delete(ref profile, App.Os_Folder, "Profile", "");

                Application.Current.MainPage = new LoginPage();



            });
        }
    }
}
