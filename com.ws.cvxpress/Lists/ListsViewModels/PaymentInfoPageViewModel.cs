using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Lists.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Lists.ListsViewModels
{
    public class PaymentInfoPageViewModel : BaseViewModel
    {
        #region Definitions

        public Command ItemsLoadCommand { get; set; }
        public Command SubmitCommand { get; set; }
        public Action PaymentDone { get; set;  }
        public Action PaymentUnDone { get; set; }
        private ObservableCollection<PaymentMethod> listDetails;
        public ObservableCollection<PaymentMethod> ListDetails
        {
            get { return listDetails; }
            set
            {
                if (Equals(value, listDetails)) return;
                listDetails = value;
                OnPropertyChanged(nameof(listDetails));
            }
        }

        private List<PaymentMethod> listDetailsObj;
        private ApiService apiService;
      

        public string ListDesc { get; set; }
        public string ListDescTo { get; set; }
        objRequesList RequesList { get; set; }

        #endregion
        public PaymentInfoPageViewModel(objRequesList requesList)
        {
            RequesList = requesList;
            ItemsLoadCommand = new Command(async () => ExecuteLoadItemsCommand());
            ItemsLoadCommand.Execute(this);
        }

        async void ExecuteLoadItemsCommand()
        {
            App.WaitScreenStart(Translator.getText("Loading"));
            apiService = new ApiService();
            ListDetails = new ObservableCollection<PaymentMethod>();
            listDetailsObj = await apiService.getPaymentMethods();

          

            foreach (PaymentMethod item in listDetailsObj)
            {
                PaymentMethod NewItem = new PaymentMethod();
                NewItem = item;
                NewItem.imageSource = (item.EntityPhoto == null) ? "bank.png" : ImageManager.BytesToImage(item.EntityPhoto);
                ListDetails.Add(item);

            }



            App.WaitScreenStop();

        }

        public async void onSubmit(int type)
        {
            App.WaitScreenStart(Translator.getText("Loading"));
            ObjListPayment objListPayment = new ObjListPayment();

            RequesList.listrequestspecs.Status = type;

            objListPayment.listRequestSpecs = RequesList.listrequestspecs;
            objListPayment.LastName = "";
            objListPayment.Name  = "";
            objListPayment.uInfo = null;

            string response = await apiService.ConfirmListPayment(objListPayment);

            App.WaitScreenStop();

            if (response == "Created") PaymentDone(); else PaymentUnDone();
          
        }



    }
}
