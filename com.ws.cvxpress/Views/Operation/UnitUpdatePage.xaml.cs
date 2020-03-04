using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using com.ws.cvxpress.ChatViews;
using Xamarin.Forms;
using com.ws.cvxpress.Helpers;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class UnitUpdatePage : ContentPage
    {
        UnitUpdateViewModel viewModel;
        ReserveItemObj RO;
        Profile profile;
        public UnitUpdatePage(ReserveItemObj Ro)
        {
            InitializeComponent();
            RO = Ro;
            from.Text = Ro.travelerSpecs.CountryCodeFrom;
            to.Text = Ro.travelerSpecs.CountryCodeTo; 
             profile = DatabaseHelper.GetProfile(App.Os_Folder);
            BindingContext = viewModel = new UnitUpdateViewModel(Ro);

            updateItemStatus(Ro.requestSpecs.status);

           

            article.Text = Ro.requestSpecs.Description;
          

            lb_frame4Func();

            void lb_frame4Func()
            {
                try
                {
                    frame4.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {

                            viewModel.frameFourAction();


                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }


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

        void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            SelectedUser ob = new SelectedUser();
            RequestSpecs IdInfo = new RequestSpecs();

            ob.travelerSpecs = RO.travelerSpecs;
            IdInfo = RO.requestSpecs;

            Navigation.PushModalAsync(new ChatPage(ob, profile.FirstName + " " + profile.LastName, RO.requestSpecs));
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            // async of the view model for travel state update
            Navigation.PopModalAsync();
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {

            viewModel.UpdateItem();

            MessagingCenter.Send<UnitUpdatePage, string>(this, "UpdateFromUnit", "Update");

            Navigation.PopModalAsync();

        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Send<UnitUpdatePage, string>(this, "DeletedItem1", "No Content");
            base.OnDisappearing();
        }

        public void updateItemStatus(int ActualStatus)
        {

            if (ActualStatus < 3)
            {
                NewPrice.IsEnabled = true;
                NewWeigth.IsEnabled = true;
            }
            else
            {
                NewPrice.IsEnabled = false;
                NewPrice.BackgroundColor = Color.LightGray;
                NewWeigth.BackgroundColor = Color.LightGray;
                NewWeigth.IsEnabled = false;
            }

            

            if (ActualStatus <= 3)
            {
                frame4.IsEnabled = false;
                viewModel.TravelState = "";
                UpdateBoton.IsEnabled = true;
            }
            else if (ActualStatus == Constants.Finished)
            {
                //bt_chat.IsEnabled = false;
                frame4.IsEnabled = false;
                UpdateBoton.IsEnabled = false;
                viewModel.TravelState = Translator.getText("ItemDelivered");
                viewModel.frameFourAction();
            }
            else if (ActualStatus == Constants.RequestAuth)
            {
                //bt_chat.IsEnabled = false;
                frame4.IsEnabled = false;
                UpdateBoton.IsEnabled = false;
                viewModel.Level4 = true;
                viewModel.TravelState = Translator.getText("WaitforAuth");


            }
            else if (ActualStatus == Constants.Confirmed)
            {
                //bt_chat.IsEnabled = false;
                frame4.IsEnabled = true;
                UpdateBoton.IsEnabled = true;
                viewModel.TravelState = Translator.getText("Authorized");
                viewModel.Level4 = true;
                viewModel.frameFourAction();

            }
        }

    }

}
