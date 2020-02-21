using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class OfferBoxViewModel : BaseViewModel
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

        private ObservableCollection<ShowTravelSpecs> lstItemsShow;
        public ObservableCollection<ShowTravelSpecs> LstItemsShow
        {

            get { return lstItemsShow; }
            set
            {
                if (Equals(value, lstItemsShow)) return;
                lstItemsShow = value;
                OnPropertyChanged(nameof(lstItemsShow));
            }



        }

        
        public bool ShowImage;

      
        public ShowTravelSpecs sts;
        public OfferBoxViewModel(int intvmstatus)
        {
            vmstatus = intvmstatus;
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            email = profile.Email;
            //Task.Run(async () => { await InitializeAsync(profile.Email); }).Wait();
            IsBusy = false;
            LoadItemsCommand = new Command(async () =>  ExecuteLoadItemsCommand());


        }


        public async Task InitializeAsync()
        {
            ExecuteLoadItemsCommand();

        }

        public async void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                App.WaitScreenStart(Translator.getText("Loading"));
                ApiService _apiService = new ApiService();
                LstItems = new ObservableCollection<TravelerSpecs>();
                LstItemsShow = new ObservableCollection<ShowTravelSpecs>();

                LstItems = await _apiService.GetTravelsAsync(email, vmstatus);
                foreach (TravelerSpecs item in LstItems)
                {


                    ShowImage = (item.DeliveredAt == "1") ? true : false;

                    try
                    {
                        if (Application.Current.Properties["notifiedItemRoute"] != null)
                        {
                            string NotifiedBoxlist = (string)Application.Current.Properties["notifiedItemRoute"];
                            string travelinf = item.CountryCodeFrom + ">" + item.CountryCodeTo;
                            if (NotifiedBoxlist.Contains(travelinf)) ShowImage = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Application.Current.Properties["notified"] = "False";

                        Application.Current.Properties["Goto"] = "";

                    }

                    item.CountryCodeFrom = item.CountryCodeFrom + " - " + ProvideCountries.GetCountryName(item.CountryCodeFrom);
                    item.CountryCodeTo = item.CountryCodeTo + " - " + ProvideCountries.GetCountryName(item.CountryCodeTo);

                  


                    sts = new ShowTravelSpecs();
                    sts.travelerSpecs = new TravelerSpecs ();
                    sts.travelerSpecs = item;
                    sts.imageShow = ShowImage;

                    LstItemsShow.Add(sts);
                }
                Application.Current.Properties["notifiedItemRoute"] = "" ;
                await Application.Current.SavePropertiesAsync();

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

    }

}


