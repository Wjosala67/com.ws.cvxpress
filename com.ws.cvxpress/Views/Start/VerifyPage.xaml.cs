using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Start
{
    public partial class VerifyPage : ContentPage
    {
        VerifyPageViewModel vm;
        public VerifyPage()
        {
            InitializeComponent();
            BindingContext = vm = new VerifyPageViewModel();

            MessagingCenter.Subscribe<VerifyPageViewModel, string>(this, "VerifyMessages", (obj, item) => {

                if(item.Split('-')[0] == Translator.getText("Error")) 
                { 
                    centerImage.Source = "atention.png";
                    this.BackgroundColor = Color.Red;
                     
                } else
                {
                    centerImage.Source = "roundcheck.png";
                    this.BackgroundColor = Color.SeaGreen;
                }
                DisplayAlert(item.Split('-')[0], item.Split('-')[1], "Ok");


            });

            lb_backFunc();
            void lb_backFunc()
            {
                try
                {
                    b_Back.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Application.Current.MainPage = new LoginPage();
                        })
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

        }

      
    }
}
