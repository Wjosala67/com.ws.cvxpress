using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;
using System.Collections.Generic;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.Lists.Models;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Classes;

namespace com.ws.cvxpress.Lists.ListsViewModels
{
    public class RequestListPageViewModel : BaseViewModel
    {
        public ObservableCollection<LRequestSpecs_det> Items { get; set; }
        public Command AddItemCommand { get; set; }

        #region Definitions
        public Action DisplayInvalidPrompt;
        public Action ItemSavedAction;
        public Action ItemNotSavedAction;
        public Action DisplayInvalidTravelObject;
        public Action DisplayTravelObjectCreated;
        public RequestSpecs requestSpecs;
        private string imagelist;
        private string Tracking;
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

        private int IdTravel { get; set; }
      
        #endregion





        public RequestListPageViewModel(string tracking, int Idtravel)
        {
            Tracking = tracking;
            IdTravel = Idtravel;
            AproxWeight = "0";
            SelectedItem = new Categories();
            lstitemcategories = new List<Categories>();
            Items = new ObservableCollection<LRequestSpecs_det>();
            AddItemCommand = new Command(async () => await AddItem());

            lstitemcategories = DatabaseHelper.getCategoriesApp(App.Os_Folder, 1);
        }

        async Task AddItem()
        {
            App.WaitScreenStart(Translator.getText("Loading"));
            ApiService _apiService = new ApiService();
            LRequestSpecs_det item = new LRequestSpecs_det();

            item.Id = IdTravel;
            item.id_det = 0;
            item.ProductImage = ImageByte;
            item.Name = ItemName;
            item.Description = ItemDesc;
            item.FindWhere = ItemFind;     
            item.ProductValue = decimal.Parse(ItemValue);
            item.Quantity = int.Parse(ItemQty);
            item.Weight = decimal.Parse(AproxWeight);
            item.Cat_Id = SelectedItem.Cat_Id;
            item.TrackingNumber = Tracking;


            string Response = await _apiService.RegisterItemListDet(item);

            App.WaitScreenStop();

           if(Response == "Created")
            {
                ItemSavedAction();
            }else
            {

            }
        }



    }

}
