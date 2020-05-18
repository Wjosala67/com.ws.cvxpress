using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Lists.Models;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Lists.ListsViewModels
{
    public class ListCodePageViewModel : BaseViewModel
    {
        #region

        public ICommand AddItemCommand { get; set;  } 
        public ICommand CheckOutCommand { get; set; }

        private ObservableCollection<string> lststates;

        public ObservableCollection<string> LstStates
        {
            get { return lststates; }
            set
            {
                if (Equals(value, lststates)) return;
                lststates = value;
                OnPropertyChanged(nameof(lststates));
            }

        }

        private string selectedstate;

        public string SelectedState
        {
            get { return selectedstate; }
            set
            {
                if (Equals(value, selectedstate)) return;
                selectedstate = value;
                OnPropertyChanged(nameof(selectedstate));
            }
        }

        private string selectedstate2;

        public string SelectedState2
        {
            get { return selectedstate2; }
            set
            {
                if (Equals(value, selectedstate2)) return;
                selectedstate2 = value;
                OnPropertyChanged(nameof(selectedstate2));
            }
        }
        public bool Confirmed { get; set;  }
        public bool NoDelete { get; set; }
        private string addresseephone;

        public string AddresseePhone
        {
            get { return addresseephone;  }
            set
            {
                if (Equals(value, addresseephone)) return;
                addresseephone = value;
                OnPropertyChanged(nameof(addresseephone));
            }
        }

        private string addressee;

        public string Addressee
        {
            get { return addressee; }
            set
            {
                if (Equals(value, addressee)) return;
                addressee = value;
                OnPropertyChanged(nameof(addressee));
            }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set
            {
                if (Equals(value, address)) return;
                address = value;
                OnPropertyChanged(nameof(address));
            }
        }

        private string city;

        public string City
        {
            get { return city; }
            set
            {
                if (Equals(value, city)) return;
                city = value;
                OnPropertyChanged(nameof(city));
            }
        }
        public Action NoDataSpecified;
        public Action AddItemAction;
        public Action CheckOutAction;
        private TravelerSpecs TravelerSpecs;

        public objRequesList CurrentAddressee;

        public Command LoadItemsCommand { get; }
       
        private ApiService apiService;

        public string txtListSelection { get; set;  }

        public string txtAmount { get; set; }
        public string txtService { get; set; }
        public string txtShipment{ get; set; }
        public string txtTotal { get; set; }
        public int totalItems { get; set;  }
        public List<LRequestSpecs_det> ListShow { get; set;  }

        #endregion
        public ListCodePageViewModel(TravelerSpecs travelerSpecs)
        {
            TravelerSpecs = travelerSpecs;
            SelectedState = string.Empty;
            apiService = new ApiService();
            ListShow = new List<LRequestSpecs_det>();
            lststates = new ObservableCollection<string>();
            totalItems = 0;
            lststates.Add("Amazonas");
            lststates.Add("Anzoátegui");
            lststates.Add("Apure");
            lststates.Add("Aragua");
            lststates.Add("Barinas");
            lststates.Add("Bolívar");
            lststates.Add("Carabobo");
            lststates.Add("Cojedes");
            lststates.Add("Delta Amacuro");
            lststates.Add("Distrito Capital");
            lststates.Add("Falcón");
            lststates.Add("Guárico");
            lststates.Add("Lara");
            lststates.Add("Mérida");
            lststates.Add("Miranda");
            lststates.Add("Monagas");
            lststates.Add("Nueva Esparta");
            lststates.Add("Portuguesa");
            lststates.Add("Sucre");
            lststates.Add("Táchira");
            lststates.Add("Trujillo");
            lststates.Add("Vargas");
            lststates.Add("Yaracuy");
            lststates.Add("Zulia");

            AddItemCommand = new Command(AddItem);

            CheckOutCommand = new Command(CheckOut);
        }

        async void CheckOut(object obj)
        {
            if(ListShow != null) { 
            if (CurrentAddressee != null && ListShow.Count > 0)
            {
                App.WaitScreenStart(Translator.getText("Loading"));

                ListRequestSpecs listRequestSpecs = CurrentAddressee.listrequestspecs;
                listRequestSpecs.Status = 1;
                
                listRequestSpecs.TotalItems = totalItems;
                listRequestSpecs.TotalProductValue = decimal.Parse(txtTotal);

               
                listRequestSpecs.TotalWeight = (from list in ListShow select list.Weight).Sum();
                string Response = await apiService.UpdateListDet(listRequestSpecs);

                if (Response == "No Content") CheckOutAction();

                App.WaitScreenStop();
             }
            }
        }

        public async Task ExecuteLoadItems()
        {
            CurrentAddressee = new objRequesList();

            App.WaitScreenStart(Translator.getText("Loading"));

            CurrentAddressee = await apiService.getTravelDefList(App.User +"|"+ TravelerSpecs.Id, TravelerSpecs.Id);

            if (CurrentAddressee != null)
            {
                selectedstate2 = CurrentAddressee.listrequestspecs.State;
                Addressee = CurrentAddressee.listrequestspecs.Addressee;
                address = CurrentAddressee.listrequestspecs.Address;
                addresseephone = CurrentAddressee.listrequestspecs.AddresseePhone;

                Confirmed = (CurrentAddressee.listrequestspecs.Status != 0) ? true : false;

                NoDelete = (Confirmed) ? false : true;

                city = CurrentAddressee.listrequestspecs.City;

                

                decimal SumTotal = 0;
                if(CurrentAddressee.lrequestspecs_det != null) {

                    totalItems = CurrentAddressee.lrequestspecs_det.Select(X => X.Quantity).Sum();
                    foreach (LRequestSpecs_det item in CurrentAddressee.lrequestspecs_det)
                {
                    SumTotal += item.Quantity * item.ProductValue;
                }


                }
                txtAmount = (SumTotal == 0)? "0.00": SumTotal.ToString();

                txtService = CurrentAddressee.listrequestspecs.ServiceFee.ToString();

                txtShipment = CurrentAddressee.listrequestspecs.ShipmentFee.ToString();

                txtTotal = (SumTotal + CurrentAddressee.listrequestspecs.ServiceFee + CurrentAddressee.listrequestspecs.ShipmentFee).ToString();




                txtListSelection = CurrentAddressee.itemsInList.ToString();

                ListShow = CurrentAddressee.lrequestspecs_det;

                if(ListShow!= null)
                foreach (LRequestSpecs_det item in ListShow)
                {


                    item.imageSource = (item.ProductImage == null)? "item.png": ImageManager.BytesToImage(item.ProductImage);
                   
                }

            }
            App.WaitScreenStop();
            MessagingCenter.Send<ListCodePageViewModel, string>(this, "DataNoData", "Done");




           
        }

        async void AddItem(object obj)
        {
            if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(addressee) || string.IsNullOrEmpty(selectedstate))
            {
                NoDataSpecified();

            }
            else
            {
                App.WaitScreenStart(Translator.getText("Loading"));
                ListRequestSpecs listRequestSpecs = new ListRequestSpecs();
                listRequestSpecs.Id = TravelerSpecs.Id;
                listRequestSpecs.CountryCodeFrom = TravelerSpecs.CountryCodeFrom.Split('-')[0];
                listRequestSpecs.CountryCodeTo = TravelerSpecs.CountryCodeTo.Split('-')[0];
                listRequestSpecs.TravelDate = TravelerSpecs.FromDate;
                listRequestSpecs.TravelLimit = TravelerSpecs.ToDate;
                listRequestSpecs.Addressee = addressee;
                listRequestSpecs.Address = address;
                listRequestSpecs.AddresseePhone = addresseephone;
                listRequestSpecs.City = city;
                listRequestSpecs.State = SelectedState;
                listRequestSpecs.Status = 0;
                listRequestSpecs.TotalItems = 0;
                listRequestSpecs.TotalProductValue = 0;
                listRequestSpecs.TotalWeight = 0;
                listRequestSpecs.TrackingNumber = App.User + "|"+TravelerSpecs.Id;
                listRequestSpecs.OpenDays = "";
                listRequestSpecs.OpenDelivery = "";
                listRequestSpecs.Email = App.User;



                CurrentAddressee = await apiService.getTravelDefList(listRequestSpecs.TrackingNumber, listRequestSpecs.Id);
               

                if (CurrentAddressee == null)
                {
                    await apiService.RegisterItemList(listRequestSpecs);
                    App.WaitScreenStop();
                    AddItemAction();
                } else
                {
                    App.WaitScreenStop();
                    AddItemAction();
                }
                
            }
        }
    }
}
