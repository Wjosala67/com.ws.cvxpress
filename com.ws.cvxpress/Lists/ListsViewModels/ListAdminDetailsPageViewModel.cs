using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Lists.Models;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Lists.ListsViewModels
{
    public class ListAdminDetailsPageViewModel: BaseViewModel
    {
        #region Definitions

        public Command ItemsLoadCommand { get; set; }
        public Command SubmitCommand { get; set;  }
        private ObservableCollection<LRequestSpecs_det> listDetails;
        public ObservableCollection<LRequestSpecs_det> ListDetails
        {
            get { return listDetails; }
            set
            {
                if (Equals(value, listDetails)) return;
                listDetails = value;
                OnPropertyChanged(nameof(listDetails));
            }
        }

        public ListDetailsObj listDetailsObj;
        private ApiService apiService;
        private ShowListsSpecs _showListsSpecs;
        private int _idTravel;
        public decimal InteriorShipment { get; set;  }
        public decimal Service { get; set; }
        public decimal TotalWeight { get; set; }
        public string ServiceDesc { get; set; }
        
        public string ListDesc { get; set; }
        public string ListDescTo { get; set; }
        #endregion
        public ListAdminDetailsPageViewModel(ShowListsSpecs showListsSpecs, int idTravel)
        {
            _showListsSpecs = showListsSpecs;
            listDetails = new ObservableCollection<LRequestSpecs_det>();
            ListDetails = new ObservableCollection<LRequestSpecs_det>();
            ListDesc = string.Empty;
            ListDescTo = string.Empty;
            Service = 0;
            _idTravel = idTravel;
            apiService = new ApiService();

            ItemsLoadCommand = new Command(async () => ExecuteLoadItemsCommand());
            

            SubmitCommand = new Command(UpdateList);

        }

        private async void UpdateList(object obj)
        {
            App.WaitScreenStart(Translator.getText("Loading"));
            ListDetailsObj newListDetailsObj = new ListDetailsObj();

            newListDetailsObj.user = listDetailsObj.user;

            foreach(LRequestSpecs_det item in ListDetails)
            {
                item.imageSource = null;
            }

            newListDetailsObj.lrequestSpecs_det = new List<LRequestSpecs_det>(ListDetails);

            listDetailsObj.listrequestSpecs.ShipmentFee = InteriorShipment;
            listDetailsObj.listrequestSpecs.ServiceFee = Service;
            listDetailsObj.listrequestSpecs.ServiceDesc = ServiceDesc;
            listDetailsObj.listrequestSpecs.TotalWeight = TotalWeight;
            listDetailsObj.listrequestSpecs.Status = 2;

            decimal totalProductValue = 0;
            int totalItems = 0;

            foreach (LRequestSpecs_det item in ListDetails)
            {
                totalProductValue += (item.Quantity * item.ProductValue);
                totalItems += item.Quantity;
            }

            listDetailsObj.listrequestSpecs.TotalProductValue = totalProductValue;
            listDetailsObj.listrequestSpecs.TotalItems = totalItems;

            newListDetailsObj.listrequestSpecs = listDetailsObj.listrequestSpecs;

           

            string response = await apiService.updateListDetails(newListDetailsObj);

            if (response == "Accepted") MessagingCenter.Send<ListAdminDetailsPageViewModel, string>(this, "ListUpdated", "Done");
            else MessagingCenter.Send<ListAdminDetailsPageViewModel, string>(this, "ListUpdated", "UnDone");
            App.WaitScreenStop();

        }

        async void ExecuteLoadItemsCommand()
        {

            listDetails = new ObservableCollection<LRequestSpecs_det>();
            ListDetails = new ObservableCollection<LRequestSpecs_det>();
            listDetailsObj = new ListDetailsObj();
            App.WaitScreenStart(Translator.getText("Loading"));
            listDetailsObj = await apiService.getListDetails(_idTravel.ToString(), _showListsSpecs.travelerSpecs.Email);


            foreach (LRequestSpecs_det item in listDetailsObj.lrequestSpecs_det)
            {
                item.imageSource = (item.ProductImage==null)? "item.png":ImageManager.BytesToImage(item.ProductImage);
                
                ListDetails.Add(item);

            }

            InteriorShipment = listDetailsObj.listrequestSpecs.ShipmentFee;

            Service = listDetailsObj.listrequestSpecs.ServiceFee;
            //TotalWeight = listDetailsObj.listrequestSpecs.ShipmentFee;
            ServiceDesc = listDetailsObj.listrequestSpecs.ServiceDesc; 

        App.WaitScreenStop();

        }
    }
}
