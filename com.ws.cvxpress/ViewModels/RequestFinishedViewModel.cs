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
    public class RequestFinishedViewModel : BaseViewModel
    {

        public Command LoadItemsCommand { get; set; }
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

        private ObservableCollection<RequestSpecs> lstItemsShow;
        public ObservableCollection<RequestSpecs> LstItemsShow
        {

            get { return lstItemsShow; }
            set
            {
                if (Equals(value, lstItemsShow)) return;
                lstItemsShow = value;
                OnPropertyChanged(nameof(lstItemsShow));
            }



        }
        private int Status { get; set; }
        public RequestFinishedViewModel(int status)
        {
            Status = status;
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            email = profile.Email;
            //Task.Run(async () => { await InitializeAsync(email); }).Wait();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());


        }

        private async Task InitializeAsync(string sEmail)
        {
            //using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {

                ApiService _apiService = new ApiService();
                LstItems = new ObservableCollection<RequestSpecs>();
                lstItemsShow = new ObservableCollection<RequestSpecs>();

                LstItems = await _apiService.GetRequestsAsync(email, Status, "", "");

                foreach (RequestSpecs item in LstItems)
                {

                    item.CountryCodeFrom = item.CountryCodeFrom + " - " + ProvideCountries.GetCountryName(item.CountryCodeFrom);
                    item.CountryCodeTo = item.CountryCodeTo + " - " + ProvideCountries.GetCountryName(item.CountryCodeTo);
                    item.imageSource = ImageManager.BytesToImage(item.ProductImage);
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
                LstItems = new ObservableCollection<RequestSpecs>();
                lstItemsShow = new ObservableCollection<RequestSpecs>();

                LstItems = await _apiService.GetRequestsAsync(email, Status, "", "");

                foreach (RequestSpecs item in LstItems)
                {

                    item.CountryCodeFrom = item.CountryCodeFrom + " - " + ProvideCountries.GetCountryName(item.CountryCodeFrom);
                    item.CountryCodeTo = item.CountryCodeTo + " - " + ProvideCountries.GetCountryName(item.CountryCodeTo);
                    item.imageSource = ImageManager.BytesToImage(item.ProductImage);
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

