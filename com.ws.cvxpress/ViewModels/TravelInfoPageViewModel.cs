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
using System.Diagnostics;
using System.Windows.Input;

namespace com.ws.cvxpress.ViewModels
{
    public class TravelInfoPageViewModel : BaseViewModel
    {

        #region Definitions
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
        private ObservableCollection<RequestSpecs> lstitemsshow2;
        public ObservableCollection<RequestSpecs> LstItemsShow2
        {

            get { return lstitemsshow2; }
            set
            {
                if (Equals(value, lstitemsshow2)) return;
                lstitemsshow2 = value;
                OnPropertyChanged(nameof(lstitemsshow2));
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

        private string capacityTaken;
        public string CapacityTaken
        {
            get { return capacityTaken; }
            set
            {
                capacityTaken = value;
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
        private string tolerance;
        public string Tolerance
        {
            get { return tolerance; }
            set
            {
                tolerance = value;
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
        private TravelInfoClass travelInfo;
        public TravelInfoClass TravelInfo
        {
            get { return travelInfo; }
            set
            {
                travelInfo = value;
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
        public ICommand SubmitCommand { protected set; get; }
        #endregion

        public TravelInfoPageViewModel(TravelerSpecs idInfo)
        {
            IdInfo = idInfo;
            requestsacepted = 0;
            from = idInfo.CountryCodeFrom;
            to = idInfo.CountryCodeTo;
            datefrom = idInfo.FromDate;
            dateto = idInfo.ToDate;
            decimal result;

            CapacityNumber = (decimal.TryParse(IdInfo.Capacity, out result)) ? decimal.Parse(IdInfo.Capacity) : 0;

            lstrequestsAccepted = new ObservableCollection<Traveler_Request>();

            capacity = Translator.getText("Capacity") ;
         
           
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            email = profile.Email;

            LoadItemsCommand = new Command(async () => await onsomecomandAsync());
            LoadItemsCommand.Execute(true);

           
        }

      
        public async Task onsomecomandAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                App.WaitScreenStart(Translator.getText("Loading"));
                ApiService _apiService = new ApiService();
                LstItems = new ObservableCollection<RequestSpecs>();
                lstitemsshow = new ObservableCollection<RequestSpecs>();
                lstitemsshow2 = new ObservableCollection<RequestSpecs>();


                LstItems.Clear();
                lstitemsshow.Clear();
                lstitemsshow2.Clear();

                travelInfo = new TravelInfoClass();

                travelInfo = await _apiService.GetInfoTravelLists(IdInfo.Id, 0, from.Split(' ')[0].Trim(), to.Split(' ')[0].Trim(), email);

                TotalCapacityAccepted = 0;

                if (travelInfo != null) { 
                foreach (RequestSpecs item in travelInfo.lstRequestSpecsNew)
                {
                    if (item.status == 0)
                    {
                        item.CountryCodeFrom = item.CountryCodeFrom + " - " + ProvideCountries.GetCountryName(item.CountryCodeFrom);
                        item.CountryCodeTo = item.CountryCodeTo + " - " + ProvideCountries.GetCountryName(item.CountryCodeTo);
                        item.imageSource = ImageManager.BytesToImage(item.ProductImage);
                        if (item.OpenDays == "Yes") item.Tolerance = Translator.getText("OpenDate");
                        if (item.OpenDelivery == "Yes") item.Tolerance = Translator.getText("rangedays");
                        lstitemsshow.Add(item);
                    }
                }


                decimal result;
                CapacityNumber = (decimal.TryParse(IdInfo.Capacity, out result)) ? decimal.Parse(IdInfo.Capacity) : 0;

               

                TotalCapacityAccepted = travelInfo.lstRequestSpecsAccepted.Select(x => x.Weight).Sum();
                }

                CapacityTaken = TotalCapacityAccepted.ToString();
                CapacityNumber = CapacityNumber - TotalCapacityAccepted;
                TextRequestsAcepted = Translator.getText("RequestsAcepted"); //+ "(" + lstrequestsAccepted.Count + ")";


                foreach (RequestSpecs item in travelInfo.lstRequestSpecsAccepted)
                {

                    item.CountryCodeFrom = item.CountryCodeFrom + " - " + ProvideCountries.GetCountryName(item.CountryCodeFrom);
                    item.CountryCodeTo = item.CountryCodeTo + " - " + ProvideCountries.GetCountryName(item.CountryCodeTo);
                    ImageSource imasource = ImageManager.BytesToImage(item.ProductImage);
                    item.imageSource = imasource;
                    item.canDelete = (IdInfo.status == 0 && item.status != 22 && item.status != 9) ? true : false;
                    lstitemsshow2.Add(item);


                }

                IsBusy = false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                App.WaitScreenStop();
                IsBusy = false;
            }

        }




      

        internal async Task<bool> ReserveItem(RequestSpecs requestSpecs, TravelerSpecs travelerSpecs)
        {

            ApiService _apiService = new ApiService();

            ReserveItemObj reserveItem = new ReserveItemObj();


            Traveler_Request traveler_RequestObj = new Traveler_Request();

            traveler_RequestObj.IdRequestSpecs = requestSpecs.Id;
            traveler_RequestObj.IdTravelerSpecs = travelerSpecs.Id;
            traveler_RequestObj.Date_Accepted = DateTime.Now;
            traveler_RequestObj.status = Constants.ItemTaken;
            traveler_RequestObj.Comments = "";
            traveler_RequestObj.Email = travelerSpecs.Email;

            requestSpecs.status = Constants.ItemTaken;

            requestSpecs.CountryCodeFrom = requestSpecs.CountryCodeFrom.Split('-')[0].Trim();
            requestSpecs.CountryCodeTo = requestSpecs.CountryCodeTo.Split('-')[0].Trim();

            reserveItem.travelerRequest = traveler_RequestObj;
            reserveItem.requestSpecs = requestSpecs;
            reserveItem.status = Constants.ItemTaken;

            bool created = await _apiService.SetItemToTravelerAsync(reserveItem);

            return created;
        }

        public async Task<ObservableCollection<RequestSpecs>> ExecuteLoadItemsCommand2()
        {
            await onsomecomandAsync();
            return lstitemsshow;
        }

        internal async Task UpdateRequestspecsSelectedAsync(RequestSpecs requestSpecs, int status)
        {
            ApiService _apiService = new ApiService();

            await _apiService.UpdateRequestSpecs(requestSpecs, status);

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

                    await onsomecomandAsync();



                }
            }
            else
            {
                MessagingCenter.Send<TravelInfoPageViewModel, string>(this, "DeletedItem1", "No Possible");
            }
        }
    }
}
