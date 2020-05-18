using System;
using System.Collections.Generic;
using System.Linq;
using com.ws.cvxpress.ChatViews;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Lists.ListsViewModels;
using com.ws.cvxpress.Lists.Models;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Lists
{
    public partial class ListCodePage : ContentPage
    {
        ListCodePageViewModel viewModel;
        TravelerSpecs _TravelerSpecs;
        ApiService apiService = new ApiService();
        decimal ppqk = 0;
        public ListCodePage(TravelerSpecs travelersSpecs)
        {
            _TravelerSpecs = travelersSpecs;
            InitializeComponent();

            List<App_Con> Lconf = DatabaseHelper.getConfiguration(App.Os_Folder);
            string strPpqk = Lconf.Where(x => x.Name == "CostoPorKilo").Select(x => x.Value).FirstOrDefault().ToString();
            ppqk = decimal.Parse(strPpqk);

            BindingContext = viewModel = new ListCodePageViewModel(travelersSpecs);
            Title = Translator.getText("DeluveryInfo");
          
            viewModel.NoDataSpecified += () => ShowResult();
            viewModel.AddItemAction += () => GoToAddItem();
            viewModel.CheckOutAction += () => ConfirmAndOut();


            
           
            MessagingCenter.Subscribe<ListCodePageViewModel, string>(this, "DataNoData", async (obj, item) => {

                if(item == "Done")
                {
                    updatepage();
                }
             

            });

            if (Convert.ToDateTime(travelersSpecs.ToDate.ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                 
                bt_AddItem.IsVisible = false;
                bt_checkout.IsVisible = false;
                return;
            }




            stateFunc();
            void stateFunc()
            {

                try
                {
                    status_image.GestureRecognizers.Add(new TapGestureRecognizer
                    {

                        Command = new Command(async () =>
                        {
                            if(viewModel.CurrentAddressee.listrequestspecs.Status == 2)
                            {
                               

                                
                                decimal amount = viewModel.CurrentAddressee.listrequestspecs.TotalProductValue +
                               viewModel.CurrentAddressee.listrequestspecs.ServiceFee +
                               viewModel.CurrentAddressee.listrequestspecs.ShipmentFee + (viewModel.CurrentAddressee.listrequestspecs.TotalWeight * ppqk);

                               
                                    await Navigation.PushModalAsync(new PaymentInfoPage(amount , viewModel.CurrentAddressee));
                               

                            }
                           


                        })
                    });

                }catch(Exception ex) { }

                chatfunc();

                void chatfunc()
                {
                    try {
                        chat.GestureRecognizers.Add(new TapGestureRecognizer
                        {
                            Command = new Command(async () =>
                           {
                               SelectedUser selectedUser = new SelectedUser();
                               App.WaitScreenStart(Translator.getText("Loading"));
                               selectedUser = await apiService.getListAcceptedbyTravelerAsync(travelersSpecs);
                               RequestSpecs requestSpecs = new RequestSpecs();
                               requestSpecs.Id = viewModel.CurrentAddressee.listrequestspecs.Id;
                               requestSpecs.Email = viewModel.CurrentAddressee.listrequestspecs.Email;
                               requestSpecs.DeliveredAt = viewModel.CurrentAddressee.listrequestspecs.DeliveredAt;
                               App.WaitScreenStop();

                               await Navigation.PushModalAsync(new ChatPage(selectedUser, selectedUser.user.FirstName + " " + selectedUser.user.LastName, requestSpecs));
                           })
                        });

                    }catch(Exception x)
                    {

                    }

                }


            }





        }

        private void updatepage()
        {
            Addresse.Text = viewModel.Addressee;
            Address.Text = viewModel.Address;
            City.Text = viewModel.City;
            AddresseePhone.Text = viewModel.AddresseePhone;


            txt_selectioncount.Text = Translator.getText("ListSelection").Replace("{0}", viewModel.totalItems.ToString());
            txt_amount.Text = Translator.getText("Amount").Replace("{0}", "");
            amount.Text = viewModel.txtAmount;


            txt_Service.Text = Translator.getText("Service") + ":";
            Service.Text = viewModel.txtService;
            if (viewModel.txtService == "0.00")
            {
                txt_Service.IsVisible = false; Service.IsVisible = false;
                txt_Total.IsVisible = false; Total.IsVisible = false;
                txt_ShipLD.IsVisible = false; ShipmentLD.IsVisible = false;

            }
            txt_Ship.Text = Translator.getText("ShipMentFee") + ":";
            txt_ShipLD.Text = Translator.getText("ShipLD") + ":";
            Shipment.Text = viewModel.txtShipment;
            if (viewModel.CurrentAddressee != null)
            {
                ShipmentLD.Text = (viewModel.CurrentAddressee.listrequestspecs.TotalWeight * ppqk).ToString();
                Total.Text = (decimal.Parse(viewModel.txtTotal) + (viewModel.CurrentAddressee.listrequestspecs.TotalWeight * ppqk)).ToString();
            }
            if (viewModel.txtShipment == "0.00")
            {
                txt_Ship.IsVisible = false; Shipment.IsVisible = false;

            }
            txt_Total.Text = "Total:";

            Title = Translator.getText("ListSelection").Replace("{0}", viewModel.txtListSelection);

            if (viewModel.Confirmed)
            {
                Addresse.IsReadOnly = true;
                Address.IsEnabled = false;
                City.IsEnabled = false;
                AddresseePhone.IsEnabled = false;
                PickerState.IsEnabled = false;

                bt_AddItem.IsVisible = false;
                bt_checkout.IsVisible = false;
                status_image.IsVisible = true;
                lb_status.IsVisible = true;

                int listStatus = viewModel.CurrentAddressee.listrequestspecs.Status;

                switch (listStatus)
                {
                    case 1:
                        status_image.Source = "clock.png";
                        lb_status.Text = Translator.getText("ListInReview");
                        break;
                    case 2:
                        status_image.Source = "card.png";
                        lb_status.Text = Translator.getText("WaitForPayment");
                        break;
                    case 3:
                        status_image.Source = "baggrey.png";
                        lb_status.Text = Translator.getText("ListInProcess");
                        break;
                    case 4:
                        status_image.Source = "baggrey.png";
                        lb_status.Text = Translator.getText("ListInBought");
                        break;
                    case 5:
                        status_image.Source = "dcheckgrey.png";
                        lb_status.Text = Translator.getText("ListCompleted");
                        break;
                    case 6:
                        status_image.Source = "cancel.png";
                        lb_status.Text = Translator.getText("ListCancelled");
                        break;

                }
                if (listStatus > 1) chat.IsVisible = true;


            }

            MyListView.ItemsSource = viewModel.ListShow;




            int c = 0;



            foreach (string items in viewModel.LstStates)
            {
                string a = viewModel.SelectedState2;
                if (a != null)
                    if (items.Contains(viewModel.SelectedState2)) PickerState.SelectedIndex = c;
                c++;
            }
        }

        async void ConfirmAndOut()
        {
            await DisplayAlert(Translator.getText("Notice"), Translator.getText("ListConfirmed"), "Ok");
             await Navigation.PopAsync();
        }

        private void GoToAddItem()
        {
            Navigation.PushAsync(new RequestListPage(App.User +"|"+ _TravelerSpecs.Id, _TravelerSpecs.Id));
        }

        private void ShowResult()
        {
            DisplayAlert(Translator.getText("Notice"), Translator.getText("ListDataRequired"), "Ok");
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
           

        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new RequestListPage(App.User+_TravelerSpecs.Id, _TravelerSpecs.Id));
        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void MyListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
        }



        protected override void OnAppearing()
        {

            viewModel.ExecuteLoadItems();

         

     




            base.OnAppearing();
        }

        async void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
        {

            if (viewModel.Confirmed) {

                await DisplayAlert(Translator.getText("Notice"), Translator.getText("DeleteNotAllowedFromList"), "Ok");


            }
            else
            { 
            var action = await DisplayAlert(Translator.getText("Notice"), Translator.getText("DeleteFromList"), Translator.getText("Yes"), Translator.getText("No"));

            if (action)
            {

                App.WaitScreenStart("Loading");
                var menuItem = sender as ImageButton;
                var selectedItem = menuItem.CommandParameter as LRequestSpecs_det;

                    ApiService apiService = new ApiService();

                    string response = await apiService.DeleteItemFList(selectedItem);

                    if (response.ToUpper() == "OK") await viewModel.ExecuteLoadItems();
                    else
                    {
                        App.WaitScreenStop();
                        await DisplayAlert(Translator.getText("Notice"), Translator.getText("TryLater"), "Ok");
                    }

            }

            }
        }

        async void  bt_checkout_Clicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel.CurrentAddressee.listrequestspecs.Status == 0)
                
                {
               
                    await Navigation.PushModalAsync(new ListsPaymentPage(viewModel.CurrentAddressee.listrequestspecs));
                }


        }




    }
}
