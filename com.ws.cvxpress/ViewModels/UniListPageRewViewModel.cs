using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class UniListPageRewViewModel : BaseViewModel
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
        public UniListPageRewViewModel(TravelerSpecs idInfo)
        {
            IdInfo = idInfo;
            requestsacepted = 0;
            from = idInfo.CountryCodeFrom +"-" + ProvideCountries.GetCountryName(idInfo.CountryCodeFrom);
            to = idInfo.CountryCodeTo + "-" + ProvideCountries.GetCountryName(idInfo.CountryCodeTo);  
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
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                lstrequestsAccepted = new ObservableCollection<Traveler_Request>();
                ApiService _apiService = new ApiService();
                LstItems = new ObservableCollection<RequestSpecs>();
                lstitemsshow = new ObservableCollection<RequestSpecs>();


                lstrequestsAccepted = await _apiService.getRequestesAcceptedbyTravelerAsync(IdInfo);
                LstItems = await _apiService.GetRequestToTravelAsync(9, from.Split('-')[0].Trim(), to.Split('-')[0].Trim(), email);
                CapacityNumber = 0;
                foreach (RequestSpecs item in LstItems)
                {



                    int intis = (from t in lstrequestsAccepted where t.IdRequestSpecs == item.Id select t).Count();

                    if (intis > 0)
                    {
                        item.CountryCodeFrom = item.CountryCodeFrom + " - " + ProvideCountries.GetCountryName(item.CountryCodeFrom);
                        item.CountryCodeTo = item.CountryCodeTo + " - " + ProvideCountries.GetCountryName(item.CountryCodeTo);
                        item.imageSource = ImageManager.BytesToImage(item.ProductImage);
                        lstitemsshow.Add(item);
                    }

                }
                decimal result = 0;

                capacityNumber = (decimal.TryParse(IdInfo.Capacity, out result)) ? decimal.Parse(IdInfo.Capacity) : 0;

                CapacityNumber = (from list in lstitemsshow select list.Weight).Sum();



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }






        }
        }

       
    }

