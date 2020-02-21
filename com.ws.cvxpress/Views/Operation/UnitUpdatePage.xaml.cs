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
            if(Ro.requestSpecs.status < 3)
            {
                NewPrice.IsEnabled = true;
                NewWeigth.IsEnabled = true;
            }else
            {
                NewPrice.IsEnabled = false;
                NewWeigth.IsEnabled = false;
            }
           
            BindingContext = viewModel = new UnitUpdateViewModel(Ro);

            if (Ro.requestSpecs.status <= 3)
            {
                frame4.IsEnabled = true;
                viewModel.TravelState = "";
                UpdateBoton.IsEnabled = true;
            }
            else if(Ro.requestSpecs.status == 9)
            {
                //bt_chat.IsEnabled = false;
                frame4.IsEnabled = false;
                UpdateBoton.IsEnabled = false;
                viewModel.TravelState = Translator.getText("ItemDelivered");
                viewModel.frameFourAction();
            }else if(Ro.requestSpecs.status == 21)
            {
                //bt_chat.IsEnabled = false;
                frame4.IsEnabled = false;
                UpdateBoton.IsEnabled = false;
                viewModel.Level4 = true;
                viewModel.TravelState = Translator.getText("WaitforAuth");
               

            }
            else if (Ro.requestSpecs.status == 22)
            {
                //bt_chat.IsEnabled = false;
                frame4.IsEnabled = true;
                UpdateBoton.IsEnabled = true;
                viewModel.TravelState = Translator.getText("Authorized");
                viewModel.Level4 = true;
                viewModel.frameFourAction();

            }

           

            article.Text = Ro.requestSpecs.Description;
            //lb_star5Func();

            //void lb_star5Func()
            //{
            //    try
            //    {
            //        bt_chat.GestureRecognizers.Add(new TapGestureRecognizer()
            //        {
            //            Command = new Command(() =>
            //            {


            //            }
            //                )
            //        });
            //    }
            //    catch (Exception ex) { Debug.WriteLine(ex); }
            //}

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
          
            Navigation.PopModalAsync();

        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Send<UnitUpdatePage, string>(this, "DeletedItem1", "No Content");
            base.OnDisappearing();
        }


    }

}
