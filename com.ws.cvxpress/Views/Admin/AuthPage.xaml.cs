using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Admin
{
    public partial class AuthPage : ContentPage
    {
        User_Session User_Sess;
        AuthPageViewModel viewModel;
        public AuthPage(User_Session user_Session)
        {
            InitializeComponent();
            BindingContext = viewModel = new AuthPageViewModel();
            User_Sess = user_Session;
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

            MessagingCenter.Subscribe<AuthPageViewModel , string>(this, "Enabled", (obj, item) => {


                if (item == "No")
                {
                    bt_auth.IsEnabled = false;
                    bt_reject.IsEnabled = false;
                }

            });

           
         



        }

        async void Handle_Auth(object sender, System.EventArgs e)
        {

            bool resp =  await viewModel.UpdateTravelerStatus(1, User_Sess);

            message(resp);
        }

        async void Handle_Reject(object sender, System.EventArgs e)
        {
            bool resp = await viewModel.UpdateTravelerStatus(0, User_Sess);
            message(resp);
        }

        async void message (bool status)
        {
            if(status) await DisplayAlert(Translator.getText("Notice"), Translator.getText("UpdateDone"), "Ok");
            else
                await DisplayAlert(Translator.getText("Notice"), Translator.getText("UpdateUnDone"), "Ok");

            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {

            viewModel.onCommand(User_Sess.Email);
         
            base.OnAppearing();
        }
    }
}
