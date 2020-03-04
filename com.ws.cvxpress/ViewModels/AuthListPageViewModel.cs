
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class AuthListPageViewModel : BaseViewModel
    {

        #region Definitions
        public Command LoadItemsCommand { get; set; }

        private ObservableCollection<User_Session> user_Session;
        public ObservableCollection<User_Session> _User_Session
        {
            get { return user_Session; }
            set
            {
                 user_Session = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public AuthListPageViewModel()
        {
           

        }

        public async Task ExecuteLoadItemsCommand()
        {
            

            App.WaitScreenStart(Translator.getText("Loading"));
            {
                _User_Session = new ObservableCollection<User_Session>();
                ApiService _apiService = new ApiService();



                _User_Session = await _apiService.getNewTravelers();

                MessagingCenter.Send<AuthListPageViewModel, string>(this, "Auth", "Done");

                




            }
            App.WaitScreenStop();

        }


    }
}
