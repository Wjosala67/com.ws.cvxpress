using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class RequestInfoPageViewModel : BaseViewModel
    {
        #region definitions
        private string from;
        public string From
        {
            get { return from; }
            set
            {
                from = value;
                OnPropertyChanged();
            }
        }
        private string to;
        public string To
        {
            get { return to; }
            set
            {
                to = value;
                OnPropertyChanged();
            }
        }
        private DateTime datefrom;
        public DateTime FromDate
        {
            get { return datefrom; }
            set
            {
                datefrom = value;
                OnPropertyChanged();
            }
        }

        private DateTime dateto;
        public DateTime ToDate
        {
            get { return dateto; }
            set
            {
                dateto = value;
                OnPropertyChanged();
            }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }
        private string wherefind;
        public string WhereFind
        {
            get { return wherefind; }
            set
            {
                wherefind = value;
                OnPropertyChanged();
            }
        }
        private decimal productvalue;
        public decimal ProductValue
        {
            get { return productvalue; }
            set
            {
                productvalue = value;
                OnPropertyChanged();
            }
        }
        private int qty;
        public int Qty
        {
            get { return qty; }
            set
            {
                qty = value;
                OnPropertyChanged();
            }
        }

        private decimal weight;
        public decimal Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged();
            }
        }

        private DateTime desireddate;
        public DateTime DesiredDate
        {
            get { return desireddate; }
            set
            {
                desireddate = value;
                OnPropertyChanged();
            }
        }
        private string imagesource1;
        public string ImageSource1
        {
            get { return imagesource1; }
            set
            {
                imagesource1 = value;
                OnPropertyChanged();
            }
        }

        private string imagesource2;
        public string ImageSource2
        {
            get { return imagesource2; }
            set
            {
                imagesource2 = value;
                OnPropertyChanged();
            }
        }
        private string imagesource22;
        public string ImageSource22
        {
            get { return imagesource22; }
            set
            {
                imagesource22 = value;
                OnPropertyChanged();
            }
        }
        private string imagesource3;
        public string ImageSource3
        {
            get { return imagesource3; }
            set
            {
                imagesource3 = value;
                OnPropertyChanged();
            }
        }
        private string imagesource4;
        public string ImageSource4
        {
            get { return imagesource4; }
            set
            {
                imagesource4 = value;
                OnPropertyChanged();
            }
        }


        private string started;
        public string Started
        {
            get { return started; }
            set
            {
                started = value;
                OnPropertyChanged();
            }
        }

        private string bought;
        public string Bought
        {
            get { return bought; }
            set
            {
                bought = value;
                OnPropertyChanged();
            }
        }

        private string firstname;
        public string FirstName
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged();
            }
        }
        private string lastname;
        public string LastName
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged();
            }
        }
       
        private string ended;
        public string Ended
        {
            get { return ended; }
            set
            {
                ended = value;
                OnPropertyChanged();
            }
        }

        private string delivered;
        public string Delivered
        {
            get { return delivered; }
            set
            {
                delivered = value;
                OnPropertyChanged();
            }
        }
        private int status;
        public int Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        private bool showclick;
        public bool ShowClick
        {
            get { return showclick; }
            set
            {
                showclick = value;
                OnPropertyChanged();
            }
        }

        private bool showcchat;
        public bool ShowChat
        {
            get { return showcchat; }
            set
            {
                showcchat = value;
                OnPropertyChanged();
            }
        }

        private SelectedUser useraccept;
        public SelectedUser UserAccept
        {
            get { return useraccept; }
            set
            {
                useraccept = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<SelectedUser> lstrequestsAccepted;
        public ObservableCollection<SelectedUser> LstRequestsAccepted
        {

            get { return lstrequestsAccepted; }
            set
            {
                if (Equals(value, lstrequestsAccepted)) return;
                lstrequestsAccepted = value;
                OnPropertyChanged(nameof(lstrequestsAccepted));
            }



        }
        private ImageSource image;
        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }
        RequestSpecs IdInfo { get; set; }
        #endregion
        ReserveItemObj RIO { get; set; }

        public RequestInfoPageViewModel(RequestSpecs idInfo)
        {
            IdInfo = idInfo;
            from = idInfo.CountryCodeFrom;
            to = idInfo.CountryCodeTo;
            datefrom = idInfo.FromDate;
            dateto = idInfo.ToDate;
            showclick = false;

            description = idInfo.Description;
            wherefind= idInfo.FindWhere;
            productvalue = idInfo.ProductValue;
            qty = idInfo.Quantity;
            weight = idInfo.Weight;

            desireddate = idInfo.ToDate;
        }
        public async Task onsomecomandAsync()
        {
            App.WaitScreenStart(Translator.getText("Loading"));

            lstrequestsAccepted = new ObservableCollection<SelectedUser>();
            ApiService _apiService = new ApiService();


            useraccept = await _apiService.getRequestesAcceptedbyTravelerAsync(IdInfo);
            Users_Ratings users_Ratings = new Users_Ratings();
            if (useraccept.user != null) 
            { 
             users_Ratings = await _apiService.getUserRatings(useraccept.travelerSpecs.Id, IdInfo.Id);

            }


            if (useraccept.user != null) 
            {
                firstname = useraccept.user.FirstName;
                lastname = useraccept.user.LastName;
                FirstName += " " + lastname;
            }
            if(useraccept.travelerSpecs != null) 
            { 
                datefrom = useraccept.travelerSpecs.FromDate;
                dateto = useraccept.travelerSpecs.ToDate;
                status = useraccept.travelerSpecs.status;
            }

            if (useraccept.user != null)
            {  
               image =(useraccept.user.UserPhoto == null) ? "giphy.gif" : ImageManager.BytesToImage(useraccept.user.UserPhoto);
            }
            


            showclick = (status == 9 || IdInfo.status == 9) ? true : false;
            showcchat = (status < 9 && IdInfo.status != 9) ? true : false;


                string message = (useraccept.travelerSpecs != null)? "User-"+ users_Ratings.Rating : "NoUser-"+ users_Ratings.Rating;

                MessagingCenter.Send<RequestInfoPageViewModel, string>(this, "UserNoUser", message);






            //}
            App.WaitScreenStop();
            //return UserAccept;
        }

        public async Task<bool> AuthCharge()
        {
            ApiService _apiService = new ApiService();
            IdInfo.status = Constants.Authorized;
            bool isAuth = await _apiService.AuthChange(IdInfo);

            // ACTUALIZA 

            return isAuth;
        }

        public async Task<bool> RefuseCharge()
        {
            ApiService _apiService = new ApiService();
            IdInfo.status = Constants.Refused;
            bool isAuth = await _apiService.AuthChange(IdInfo);

            return isAuth;
        }
    }
}
