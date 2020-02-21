using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using Xamarin.Forms;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;

namespace com.ws.cvxpress.ViewModels
{
    public class RequestPageViewModel : BaseViewModel
    {
        #region Definitions
        public Action DisplayInvalidPrompt;
        public Action DisplayInvalidTravelObject;
        public Action DisplayTravelObjectCreated;
        public RequestSpecs requestSpecs;
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


        private string itemname;
        public string ItemName
        {
            get { return itemname; }
            set
            {
                itemname = value;
                OnPropertyChanged();
            }
        }
        private string totalmessage;
        public string TotalMessage
        {
            get { return totalmessage; }
            set
            {
                totalmessage = value;
                OnPropertyChanged();
            }
        }
        private Categories selecteditem;
        public Categories SelectedItem
        {
            get { return selecteditem; }
            set
            {
                selecteditem = value;
                OnPropertyChanged();
            }
        }
        private string itemdesc;
        public string ItemDesc
        {
            get { return itemdesc; }
            set
            {
                itemdesc = value;
                OnPropertyChanged();
            }
        }
        private string itemfind;
        public string ItemFind
        {
            get { return itemfind; }
            set
            {
                itemfind = value;
                OnPropertyChanged();
            }
        }
        private string itemqty;
        public string ItemQty
        {
            get { return itemqty; }
            set
            {
                itemqty = value;
                OnPropertyChanged();
            }
        }

        private string itemvalue;
        public string ItemValue
        {
            get { return itemvalue; }
            set
            {
                itemvalue = value;
                OnPropertyChanged();
            }
        }
        private List<Categories> lstitemcategories;

        public List<Categories> LstItemCategories
        {
            get { return lstitemcategories; }
            set
            {
                if (Equals(value, lstitemcategories)) return;
                lstitemcategories = value;
                OnPropertyChanged(nameof(lstitemcategories));
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
        private string itemweight;
        public string AproxWeight
        {
            get { return itemweight; }
            set
            {
                itemweight = value;
                OnPropertyChanged();
            }
        }
        private string opendays;
        public string OpenDays
        {
            get { return opendays; }
            set
            {
                opendays = value;
                OnPropertyChanged();
            }
        }
        private string opendelivery;
        public string OpenDelivery
        {
            get { return opendelivery; }
            set
            {
                opendelivery = value;
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

        private Byte[] imagebyte;
        public Byte[] ImageByte
        {
            get { return imagebyte; }
            set
            {



                if (Equals(value, imagebyte)) return;
                imagebyte = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ICommand SubmitCommand { protected set; get; }
        List<App_Con> lstSetting;
        string stvalorporpeso;
        string strexocalorart;
        public decimal ValorPorPeso;
        public decimal RecoxValorArt;

        public RequestPageViewModel()
        {
            lstSetting  = DatabaseHelper.getConfiguration(App.Os_Folder);
           

            requestSpecs = new RequestSpecs();
            selectedCountry = new Countries();
            selectedCountryto = new Countries();
            selectedarrivaldate = DateTime.Now;
            selecteddeparturedate = DateTime.Now;
            itemqty = "1";
            Application.Current.Properties[Constants.Tspecs] = "";
            Application.Current.SavePropertiesAsync();


            lstitemcountries = new ObservableCollection<Countries>();
            lstitemcountriesto = new ObservableCollection<Countries>();

            ObservableCollection<Countries> lstc = new ObservableCollection<Countries>();
            //Get Profile



            lstitemcountries = DatabaseHelper.getCountries(App.Os_Folder);
            lstitemcountriesto = DatabaseHelper.getCountries(App.Os_Folder);

            if (lstitemcountries.Count > 0)
            {

                int cindx = 0;
                foreach (var item in lstitemcountries)
                {
                    if (SelectedCountry != null)
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


           

            lstitemcategories = new List<Categories>();

            List<Categories> lstViewA = new List<Categories>();


            //Get Profile



            lstitemcategories = DatabaseHelper.getCategoriesApp(App.Os_Folder, 1);

            //if (lstitemcategories.Count > 0 )
            //{
            //    SelectedCountry = (from c in lstitemcategories
            //                       where c.CountryCode == profile.CountryCode
            //                       select c).First();
            //    phonepattern = SelectedCountry.NumberPattern;

            //    int cindx = 0;
            //    foreach (var item in lstitemcategories)
            //    {

            //        if (item.CountryCode == SelectedCountry.CountryCode)
            //        {

            //            index = cindx;
            //        }
            //        cindx++;
            //    }
            //}

            //SubmitCommand = new Command(OnSubmit);

        }

        public async Task OnSubmit()
        {


            App.WaitScreenStart(Translator.getText("Loading"));

            try {
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

           

           

            requestSpecs.CountryCodeFrom = (selectedCountry != null) ? selectedCountry.CountryCode : null;
            requestSpecs.CountryCodeTo = (selectedCountryto != null) ? selectedCountryto.CountryCode : null;



            requestSpecs.FromDate = selecteddeparturedate;
            requestSpecs.ToDate = selectedarrivaldate;
            requestSpecs.Created = DateTime.Now;
            requestSpecs.Email = profile.Email;
            requestSpecs.status = 0;
            requestSpecs.Description = itemname;
            requestSpecs.FindWhere = itemfind;
            requestSpecs.Comments = itemdesc;
            requestSpecs.Cat_Id = (SelectedItem != null) ?SelectedItem.Cat_Id : 0;
            requestSpecs.ProductImage = ImageByte;
            requestSpecs.OpenDelivery = opendelivery;
            requestSpecs.OpenDays = opendays;
            decimal result = 0;
            int res = 0;
            int qty = (int.TryParse(itemqty, out res)) ? int.Parse(itemqty) : 1;
            requestSpecs.Quantity = qty;


            result = 0;
            requestSpecs.ProductValue = decimal.TryParse(itemvalue, out result) ? decimal.Parse(itemvalue) : 0;
          

            if ( selectedCountry.CountryCode == null || 
                 SelectedCountryTo.CountryCode == null || 
                string.IsNullOrEmpty(itemname) ||
                string.IsNullOrEmpty(itemfind) || string.IsNullOrEmpty(itemvalue) ||
                string.IsNullOrEmpty(itemdesc)|| requestSpecs.Cat_Id == 0 || requestSpecs.ProductValue == 0)
            {
                DisplayInvalidTravelObject();
                    App.WaitScreenStop();
                    return;
            }



           

           // using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {

                //if (Application.Current.Properties.ContainsKey(Constants.Sspecs))
                //{

                //    stravelerSpecs = (string)Application.Current.Properties[Constants.Sspecs];

                //    if (stravelerSpecs.Contains("|"))
                //    {

                //        requestSpecs.Weight = (decimal.TryParse(stravelerSpecs.Split('|')[0].ToString(), out result)) ? decimal.Parse(stravelerSpecs.Split('|')[0]) : 0; ;
                //        requestSpecs.Width = (decimal.TryParse(stravelerSpecs.Split('|')[1].ToString(), out result)) ? decimal.Parse(stravelerSpecs.Split('|')[1]) : 0; ;
                //        requestSpecs.Height = (decimal.TryParse(stravelerSpecs.Split('|')[2].ToString(), out result)) ? decimal.Parse(stravelerSpecs.Split('|')[2]) : 0; ;
                //        requestSpecs.Long = (decimal.TryParse(stravelerSpecs.Split('|')[3].ToString(), out result)) ? decimal.Parse(stravelerSpecs.Split('|')[3]) : 0; ;
                //        requestSpecs.DeliveredAt = stravelerSpecs.Split('|')[4];
                //        int intresult = 0;
                //        requestSpecs.Quantity = (int.TryParse(stravelerSpecs.Split('|')[5], out intresult)) ? int.Parse(stravelerSpecs.Split('|')[5]): 1;
                //    }
                //}
                decimal intresult = 0;
                requestSpecs.DeliveredAt = "";
                requestSpecs.Quantity = qty;
                requestSpecs.Weight = decimal.TryParse(AproxWeight, out intresult) ? decimal.Parse(AproxWeight) :  0;

                    #region Calculate the reward using Product value and Volume
                    decimal reward = Functions.getReward(requestSpecs.ProductValue,  requestSpecs.Weight,  requestSpecs.Quantity);
                    decimal commission = Functions.getDeliveryPrice(requestSpecs.ProductValue, requestSpecs.Weight, qty);

                    requestSpecs.Commission = commission;

                #endregion

                requestSpecs.Reward = reward;

                if (string.IsNullOrEmpty(requestSpecs.CountryCodeFrom) ||
                   string.IsNullOrEmpty(requestSpecs.CountryCodeTo) ||

                   string.IsNullOrEmpty(requestSpecs.FindWhere) ||

                   requestSpecs.ToDate == DateTime.Now )
                {

                    DisplayInvalidTravelObject();
                        App.WaitScreenStop();
                    }
                else
                {

                    //ApiService apiService = new ApiService();

                    //string created = await apiService.RegisterRequestSpecs(requestSpecs);



                    //if (created == "Created")
                    {
                     //   DisplayTravelObjectCreated();
                    }



                }
            }
            }
            catch (Exception ex) {

                App.WaitScreenStop();
            }
            finally {
                App.WaitScreenStop();
            }

            MessagingCenter.Send<RequestPageViewModel>(this, "SendObjectToPayment");

        }
    }
}

