using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class OfferBoxFinishedViewModel : BaseViewModel
    {

        public Command LoadItemsCommand { get; set; }
        private int vmstatus;

        public int VMStatus
        {
            get { return vmstatus; }
            set
            {
                vmstatus = value;
                OnPropertyChanged();
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
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
        public Uri UserImage { get; set; }

        private ObservableCollection<TravelerSpecs> lstItems;



        public ObservableCollection<TravelerSpecs> LstItems
        {

            get { return lstItems; }
            set
            {
                if (Equals(value, lstItems)) return;
                lstItems = value;
                OnPropertyChanged(nameof(lstItems));
            }



        }

        private ObservableCollection<TravelerSpecs> lstItemsShow;
        public ObservableCollection<TravelerSpecs> LstItemsShow
        {

            get { return lstItemsShow; }
            set
            {
                if (Equals(value, lstItemsShow)) return;
                lstItemsShow = value;
                OnPropertyChanged(nameof(lstItemsShow));
            }



        }

        public OfferBoxFinishedViewModel(int intvmstatus)
        {
            vmstatus = intvmstatus;
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            email = profile.Email;
            //Task.Run(async () => { await InitializeAsync(profile.Email); }).Wait();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());


        }


        public async Task InitializeAsync()
        {
            //using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {

                ApiService _apiService = new ApiService();
                LstItems = new ObservableCollection<TravelerSpecs>();
                LstItemsShow = new ObservableCollection<TravelerSpecs>();
                LstItems = await _apiService.GetTravelsAsync(email, vmstatus);

                foreach (TravelerSpecs item in LstItems)
                {

                    item.CountryCodeFrom = item.CountryCodeFrom + " - " + ProvideCountries.GetCountryName(item.CountryCodeFrom);
                    item.CountryCodeTo = item.CountryCodeTo + " - " + ProvideCountries.GetCountryName(item.CountryCodeTo);

                    LstItemsShow.Add(item);
                }


            }

        }
        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ApiService _apiService = new ApiService();
                LstItems = new ObservableCollection<TravelerSpecs>();
                lstItemsShow = new ObservableCollection<TravelerSpecs>();

                LstItems = await _apiService.GetTravelsAsync(email, vmstatus);
                foreach (TravelerSpecs item in LstItems)
                {

                    item.CountryCodeFrom = item.CountryCodeFrom + " - " + ProvideCountries.GetCountryName(item.CountryCodeFrom);
                    item.CountryCodeTo = item.CountryCodeTo + " - " + ProvideCountries.GetCountryName(item.CountryCodeTo);

                    LstItemsShow.Add(item);
                }

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
