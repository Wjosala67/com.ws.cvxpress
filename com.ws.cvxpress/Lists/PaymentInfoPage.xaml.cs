using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Lists.ListsViewModels;
using com.ws.cvxpress.Lists.Models;
using com.ws.cvxpress.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace com.ws.cvxpress.Lists
{
    public partial class PaymentInfoPage : ContentPage
    {
        PaymentInfoPageViewModel viewModel;
        objRequesList ObjRequestList;
        public PaymentInfoPage( decimal amount, objRequesList requestList)
        {
            InitializeComponent();
            ObjRequestList = requestList;
            BindingContext = viewModel = new PaymentInfoPageViewModel(requestList);


            if(requestList.listrequestspecs.TotalProductValue > 2000)
            {
                Option2.IsVisible = true;
                Option1.IsVisible = false;
                bt_accept.IsVisible = false;
                lb_SendbyEmail.IsVisible = true;

            } else if(requestList.listrequestspecs.TotalProductValue < 2000 && amount < 5000)
            {

                Option2.IsVisible = false;
                Option1.IsVisible = true;
                updatepage(requestList);
                lb_SendbyEmail.IsVisible = false;

            } else if (requestList.listrequestspecs.TotalProductValue < 2000 && amount > 5000)
            {
                Option2.IsVisible = true;
                Option1.IsVisible = false;
                bt_accept.IsVisible = false;
                lb_SendbyEmail.IsVisible = true;
            }


           



            viewModel.PaymentDone += () =>  DoneAction();
            viewModel.PaymentUnDone += () => UnDoneAction();
            lb_backFunc();
            void lb_backFunc()
            {
                try
                {
                    Back.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Navigation.PopModalAsync();
                        })
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
           
        }

        async void  DoneAction()
        {
            await DisplayAlert(Translator.getText("Notice"), Translator.getText("UpdateDone"), "OK");
            await Navigation.PopModalAsync();
        }
        
        async void UnDoneAction()
        {
            await DisplayAlert(Translator.getText("Notice"), Translator.getText("UpdateUnDone"), "OK");
            await Navigation.PopModalAsync();
        }

        void MyListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {


        }

        private void updatepage(objRequesList requestList)
        {
            List<App_Con> Lconf = DatabaseHelper.getConfiguration(App.Os_Folder);
            string strPpqk = Lconf.Where(x => x.Name == "CostoPorKilo").Select(x => x.Value).FirstOrDefault().ToString();
            decimal ppqk = decimal.Parse(strPpqk);


            decimal weightxKilo = requestList.listrequestspecs.TotalWeight * ppqk;

            txt_amount.Text = Translator.getText("AmountItems").Replace("{0}", "");
            amount.Text = requestList.listrequestspecs.TotalProductValue.ToString();

            txt_Info.Text = Translator.getText("TooLow");


            txt_Service.Text = Translator.getText("Description") + ":";
            Service.Text = requestList.listrequestspecs.ServiceDesc;

            Service_txt.Text = Translator.getText("Service") + ":";
            Service_amount.Text = requestList.listrequestspecs.ServiceFee.ToString();

            txt_Ship.Text = Translator.getText("ShipMentFee") + ":";
            txt_ShipLD.Text = Translator.getText("ShipLD") + ":";

            Shipment.Text = requestList.listrequestspecs.ShipmentFee.ToString();
            
            ShipmentLD.Text = requestList.listrequestspecs.TotalWeight  + " Kg: " +(requestList.listrequestspecs.TotalWeight * 399).ToString();

            Total.Text = (requestList.listrequestspecs.TotalProductValue+
                      requestList.listrequestspecs.ShipmentFee +
                      requestList.listrequestspecs.ServiceFee +
                      (weightxKilo)).ToString();
            
           
            txt_Total.Text = "Total:";


            int c = 0;



        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.onSubmit(3);
        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            viewModel.onSubmit(6);
        }

         void lb_SendbyEmail_Clicked(System.Object sender, System.EventArgs e)
        {
           
                Device.OpenUri(new Uri("mailto:info@passbox.com.mx?subject=Comprobande de pago " + ObjRequestList.listrequestspecs.TrackingNumber.Split('|')[1] + " - " + ObjRequestList.listrequestspecs.Id + "&body= Adjunte el comprobande de pago. Id Viaje: " + ObjRequestList.listrequestspecs.TrackingNumber.Split('|')[1] + " - Id Lista: " + ObjRequestList.listrequestspecs.Id));
            
        }
    }
}
