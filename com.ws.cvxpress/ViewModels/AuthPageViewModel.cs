using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class AuthPageViewModel : BaseViewModel
    {

        public User_Ident userIdent;
        ApiService apiService = new ApiService();
        private ImageSource _imageSource;
        public ImageSource _ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<User_Ident> lstIdents;
        public ObservableCollection<User_Ident> LstIdents
        {
            get { return lstIdents; }
            set
            {
                lstIdents = value;
                OnPropertyChanged("lstIdents");
            }
        }
        public AuthPageViewModel()
        {
            lstIdents = new ObservableCollection<User_Ident>();

        }

        public async void onCommand(string email)
        {
            App.WaitScreenStart(Translator.getText("Loading"));
           
            lstIdents = await apiService.getUserIdent(email);

            if(lstIdents.Count > 0)
            {
                _ImageSource = ImageManager.BytesToImage(lstIdents.ToList().First().Image);

            }
            else
            {
                MessagingCenter.Send<AuthPageViewModel, string>(this, "Enabled", "No");

            }

            App.WaitScreenStop();
        }

        public async Task<bool> UpdateTravelerStatus(int status, User_Session user_Session)
        {
            App.WaitScreenStart(Translator.getText("Loading"));
           bool resp =  await apiService.UpdateTravelerStatus(status, user_Session);
            App.WaitScreenStop();
           return resp;
        }
    }
}
