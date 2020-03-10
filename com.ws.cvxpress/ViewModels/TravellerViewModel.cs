using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class TravellerViewModel : BaseViewModel
    {

        #region Definitions
        public Action DisplayInvalidPrompt;
        public Action DisplayInvalidTravelObject;
        public Action DisplayInvalidDates;
        public Action DisplayInvalidDepArr;
        public TravelerSpecs travelSpecs { get; set; }
        public int id_document_type { get; set; }
        public Command LoadItemsCommand { get; set; }
        private bool isTravelerAuth;
        public bool IsTravelerAuth
        {
            get { return isTravelerAuth; }
            set
            {
                isTravelerAuth = value;
                OnPropertyChanged();
            }
        }

        private string imagelist;
        public string ImageList
        {
            get { return imagelist; }
            set
            {
                imagelist = value;
                OnPropertyChanged();
            }
        }
        private string weight;
        public string Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Countries> lstitemcountries;

        public ObservableCollection<Countries> LstitemCountries
        {
            get { return lstitemcountries; }
            set
            {
                if (Equals(value, lstitemcountries)) return;
                lstitemcountries = value;
                OnPropertyChanged(nameof(lstitemcountries));
            }

        }

        private ObservableCollection<Countries> lstitemcountriesto;

        public ObservableCollection<Countries> LstitemCountriesTo
        {
            get { return lstitemcountriesto; }
            set
            {
                if (Equals(value, lstitemcountriesto)) return;
                lstitemcountriesto = value;
                OnPropertyChanged(nameof(lstitemcountriesto));
            }

        }
        private ObservableCollection<User_Ident> lstitemident;

        public ObservableCollection<User_Ident> LstItemIdent
        {
            get { return lstitemident; }
            set
            {
                if (Equals(value, lstitemident)) return;
                lstitemident = value;
                OnPropertyChanged(nameof(lstitemident));
            }

        }

        private Countries selectedCountry;
        public Countries SelectedCountry
        {
            get { return selectedCountry; }
            set
            {



                if (Equals(value, selectedCountry)) return;
                selectedCountry = value;
                OnPropertyChanged();
            }
        }

        private DateTime selecteddeparturedate;
        public DateTime SelectedDepartureDate
        {
            get { return selecteddeparturedate; }
            set
            {



                if (Equals(value, selecteddeparturedate)) return;
                selecteddeparturedate = value;
                OnPropertyChanged();
            }
        }

        private DateTime selectedarrivaldate;
        public DateTime SelectedArrivalDate
        {
            get { return selectedarrivaldate; }
            set
            {



                if (Equals(value, selectedarrivaldate)) return;
                selectedarrivaldate = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan timeDeparture;
        public TimeSpan TimeDeparture
        {
            get { return timeDeparture; }
            set
            {



                if (Equals(value, timeDeparture)) return;
                timeDeparture = value;
                OnPropertyChanged();
            }
        }
        private TimeSpan timeArrival;
        public TimeSpan TimeArrival
        {
            get { return timeArrival; }
            set
            {



                if (Equals(value, timeArrival)) return;
                timeArrival = value;
                OnPropertyChanged();
            }
        }
        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged();
            }
        }
        private Countries selectedCountryto;
        public Countries SelectedCountryTo
        {
            get { return selectedCountryto; }
            set
            {



                if (Equals(value, selectedCountryto)) return;
                selectedCountryto = value;
                OnPropertyChanged();
            }
        }

        private int indexto;
        private string stravelerSpecs;

        public int IndexTo
        {
            get { return indexto; }
            set
            {
                indexto = value;
                OnPropertyChanged();
            }
        }

        private DateTime mindate;
        public DateTime MinDate
        {
            get { return mindate; }
            set
            {
                mindate = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ICommand SubmitCommand { protected set; get; }


        public TravellerViewModel()
        {
            imagelist = "redlist.png";

            travelSpecs = new TravelerSpecs();
            selectedCountry = new Countries();
            selectedCountryto = new Countries();
            selectedarrivaldate = DateTime.Now;
            selecteddeparturedate = DateTime.Now;
            timeArrival = new TimeSpan(12, 00, 00);
            timeDeparture = new TimeSpan(12, 00, 00);
            //weight = "0.0";
            mindate = DateTime.Now;
            Application.Current.Properties[Constants.Tspecs] = "";
            Application.Current.SavePropertiesAsync();


            lstitemcountries = new ObservableCollection<Countries>();
            lstitemcountriesto = new ObservableCollection<Countries>();

            ObservableCollection<Countries> lstc = new ObservableCollection<Countries>();
            //Get Profile

            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            //LoadItemsCommand.Execute(true);


            lstitemcountries = DatabaseHelper.getCountries(App.Os_Folder);
            lstitemcountriesto = DatabaseHelper.getCountries(App.Os_Folder);

            if (lstitemcountries.Count > 0 )
            {
               
                int cindx = 0;
                foreach (var item in lstitemcountries)
                {
                    if(SelectedCountry != null)
                    if (item.CountryCode == SelectedCountry.CountryCode)
                    {

                        index = cindx;
                    }
                    cindx++;
                }
            }

            if (lstitemcountriesto.Count > 0)
            {

                int cindxto = 0;
                foreach (var item in lstitemcountriesto)
                {
                    if (SelectedCountryTo != null)
                        if (item.CountryCode == SelectedCountryTo.CountryCode)
                        {

                            indexto = cindxto;
                        }
                    cindxto++;
                }
            }

            SubmitCommand = new Command(OnSubmit);

          
            

        }

        async void OnSubmit(object obj)
        {

            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                ApiService api = new ApiService();

                DateTime Dpt = Convert.ToDateTime(selecteddeparturedate.ToShortDateString() + " " + timeDeparture.ToString());
                DateTime Dpa = Convert.ToDateTime(selectedarrivaldate.ToShortDateString() + " " + timeArrival.ToString());


                Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
                travelSpecs.CountryCodeFrom = (selectedCountry != null) ? selectedCountry.CountryCode : null;
                travelSpecs.CountryCodeTo = (selectedCountryto != null) ? selectedCountryto.CountryCode : null;
                travelSpecs.FromDate = Dpt;
                travelSpecs.ToDate = Dpa;
                travelSpecs.Created = DateTime.Now;
                travelSpecs.Email = profile.Email;
                travelSpecs.status = 0;

                if (selectedCountry != null)
                {
                    if (selectedCountry.CountryCode == selectedCountryto.CountryCode)
                    {
                        DisplayInvalidDepArr();
                        return;
                    }
                }
                else
                {

                }
                using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                {

                    if (Application.Current.Properties.ContainsKey(Constants.UserType))
                    {

                        stravelerSpecs = (string)Application.Current.Properties[Constants.Tspecs];

                        //if (stravelerSpecs.Contains("|")) 
                        {
                            decimal result = 0;
                            travelSpecs.Capacity = (decimal.TryParse(weight, out result)) ? decimal.Parse(weight).ToString() : "0";
                            travelSpecs.DeliveredAt = "";
                        }
                    }


                    if (string.IsNullOrEmpty(travelSpecs.CountryCodeFrom) ||
                       string.IsNullOrEmpty(travelSpecs.CountryCodeTo) ||

                       string.IsNullOrEmpty(travelSpecs.Capacity) ||
                       travelSpecs.FromDate == DateTime.Now ||
                       travelSpecs.ToDate == DateTime.Now || string.IsNullOrEmpty(weight))
                    {

                        DisplayInvalidTravelObject();
                        return;
                    }
                    else
                    {

                        ApiService apiService = new ApiService();


                        // No Id returns message
                        lstitemident = await apiService.getUserIdent(profile.Email);

                        if (lstitemident.Count == 0)
                        {
                            DisplayInvalidPrompt();
                            return;
                        }

                        string created = await apiService.RegisterTravelSpecs(travelSpecs);

                        Application.Current.Properties[Constants.Tspecs] = "";
                        await Application.Current.SavePropertiesAsync();

                        MessagingCenter.Send<TravellerViewModel, string>(this, "SaveSpecs", "Done");

                    }
                }

           }  
            else { App.ToastMessage(Translator.getText("NoInternet"), Color.Red, ""); }
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                App.WaitScreenStart(Translator.getText("Loading"));
                ApiService _apiService = new ApiService();
                Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

                IsTravelerAuth =  await _apiService.getTravelerAuthStatus(profile.Email);
                MessagingCenter.Send<TravellerViewModel, string>(this, "Specs", "Done");
                
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
    }
}
