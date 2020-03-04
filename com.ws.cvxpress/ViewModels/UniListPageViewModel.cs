using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using WSViews.Classes;
using Xamarin.Forms;
using System.Linq;
using Acr.UserDialogs;

namespace com.ws.cvxpress.ViewModels
{
    public class UniListPageViewModel : BaseViewModel
    {

        #region
        public Command LoadItemsCommand { get; set; }
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
        public DateTime DateFrom
        {
            get { return datefrom; }
            set
            {
                datefrom = value;
                OnPropertyChanged();
            }
        }

        private DateTime dateto;
        public DateTime DateTo
        {
            get { return dateto; }
            set
            {
                dateto = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RequestSpecs> lstItems;
        public ObservableCollection<RequestSpecs> LstItems
        {

            get { return lstItems; }
            set
            {
                if (Equals(value, lstItems)) return;
                lstItems = value;
                OnPropertyChanged(nameof(lstItems));
            }



        }

        private ObservableCollection<RequestSpecs> lstitemsshow;
        public ObservableCollection<RequestSpecs> LstItemsShow
        {

            get { return lstitemsshow; }
            set
            {
                if (Equals(value, lstitemsshow)) return;
                lstitemsshow = value;
                OnPropertyChanged(nameof(lstitemsshow));
            }



        }

        private ObservableCollection<Traveler_Request> lstrequestsAccepted;
        public ObservableCollection<Traveler_Request> LstRequestsAccepted
        {

            get { return lstrequestsAccepted; }
            set
            {
                if (Equals(value, lstrequestsAccepted)) return;
                lstrequestsAccepted = value;
                OnPropertyChanged(nameof(lstrequestsAccepted));
            }



        }


        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }



        private string capacity;
        public string Capacity
        {
            get { return capacity; }
            set
            {
                capacity = value;
                OnPropertyChanged();
            }
        }
        private string reward;
        public string Reward
        {
            get { return reward; }
            set
            {
                reward = value;
                OnPropertyChanged();
            }
        }
        private decimal requestsacepted;
        public decimal RequestsAcepted
        {
            get { return requestsacepted; }
            set
            {
                requestsacepted = value;
                OnPropertyChanged();
            }
        }


        private string textrequestsacepted;
        public string TextRequestsAcepted
        {
            get { return textrequestsacepted; }
            set
            {
                textrequestsacepted = value;
                OnPropertyChanged();
            }
        }

        private decimal capacityNumber;
        public decimal CapacityNumber
        {
            get { return capacityNumber; }
            set
            {
                capacityNumber = value;
                OnPropertyChanged();
            }
        }

        private decimal totalcapacityaccepted;
        public decimal TotalCapacityAccepted
        {
            get { return totalcapacityaccepted; }
            set
            {
                totalcapacityaccepted = value;
                OnPropertyChanged();
            }
        }

        TravelerSpecs IdInfo;
        #endregion

        public UniListPageViewModel(TravelerSpecs idInfo)
        {
            IdInfo = idInfo;
            requestsacepted = 0;
            from = idInfo.CountryCodeFrom;
            to = idInfo.CountryCodeTo;
            datefrom = idInfo.FromDate;
            dateto = idInfo.ToDate;
           
            capacity = Translator.getText("Capacity");
           
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            email = profile.Email;
          

            LoadItemsCommand = new Command(async () => await onsomecomandAsync());
            LoadItemsCommand.Execute(true);

        }

    

        public async Task onsomecomandAsync()
        {
            App.WaitScreenStart(Translator.getText("Loading"));
            {
                lstrequestsAccepted = new ObservableCollection<Traveler_Request>();
                ApiService _apiService = new ApiService();
                LstItems = new ObservableCollection<RequestSpecs>();
                ObservableCollection<RequestSpecs>  LstItemstemp = new ObservableCollection<RequestSpecs>();
                lstitemsshow = new ObservableCollection<RequestSpecs>();
               

                lstrequestsAccepted = await _apiService.getRequestesAcceptedbyTravelerAsync(IdInfo);
                LstItems = await _apiService.GetRequestToTravelAsync(1, from.Split(' ')[0].Trim(), to.Split(' ')[0].Trim(), email);
                LstItemstemp = await _apiService.GetRequestToTravelAsync(9, from.Split(' ')[0].Trim(), to.Split(' ')[0].Trim(), email);

                foreach (RequestSpecs item in LstItemstemp) {
                    LstItems.Add(item);


                 }

                CapacityNumber = 0;
                foreach (RequestSpecs item in LstItems)
                {



                    int intis = (from t in lstrequestsAccepted where t.IdRequestSpecs == item.Id select t).Count();

                    if (intis > 0)
                    {
                        item.CountryCodeFrom = item.CountryCodeFrom + " - " + ProvideCountries.GetCountryName(item.CountryCodeFrom);
                        item.CountryCodeTo = item.CountryCodeTo + " - " + ProvideCountries.GetCountryName(item.CountryCodeTo);
                        item.imageSource = ImageManager.BytesToImage(item.ProductImage);
                        item.canDelete = (IdInfo.status == 0)? true : false;
                        lstitemsshow.Add(item);
                    }

                }
                decimal result = 0;

                capacityNumber = (decimal.TryParse(IdInfo.Capacity, out result)) ? decimal.Parse(IdInfo.Capacity) : 0;

              


                TotalCapacityAccepted = 0;
                foreach (Traveler_Request item in lstrequestsAccepted.ToList())
                {
                    //TotalCapacityAccepted += (from t in LstItems.ToList() where t.Id == item.IdRequestSpecs select t.Weight).FirstOrDefault<decimal>();
                    TotalCapacityAccepted += LstItems.ToList().Where(x => x.Id == item.IdRequestSpecs).Select(x => x.Weight).FirstOrDefault<decimal>();
                }


                CapacityNumber = TotalCapacityAccepted;




            }
            App.WaitScreenStop();
        }


        public async void DeleteItem(ReserveItemObj ro)
        {
            string responde;

            if (ro.travelerSpecs.status == 0)
            {
                using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                {
                    ApiService _apiService = new ApiService();

                    responde = await _apiService.DeleteItemFTravel(ro);

                }

                if (responde == "NoContent")
                {
                   
                    MessagingCenter.Send<UniListPageViewModel, string>(this, "DeletedItem1", "No Content");



                }
            } else
            {
                MessagingCenter.Send<UniListPageViewModel, string>(this, "DeletedItem1", "No Possible");
            }
        }
    }
}
