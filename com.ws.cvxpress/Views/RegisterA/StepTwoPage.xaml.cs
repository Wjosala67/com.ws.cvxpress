using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.RegisterA
{
    public partial class StepTwoPage : ContentPage
    {

        StepTwoViewModel viewModel;
        private string CalledFrom { get; set; }
        public StepTwoPage(string calledFrom)
        {
            InitializeComponent();
            CalledFrom = calledFrom;
            BindingContext = viewModel = new StepTwoViewModel(calledFrom);
            viewModel.NoRegisterCall += () => showResult();
            MessagingCenter.Subscribe<StepTwoViewModel, string>(this, "StepTwo", (sender, arg) => {
                DisplayAlert(Translator.getText("Notice"), arg, "Ok");
            });




            lb_BackFunc();
            void lb_BackFunc()
            {
                try
                {
                    Back.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {

                            if (CalledFrom == Constants.RegisterCall)
                            {
                                Application.Current.MainPage = new StepOnePage(Constants.RegisterCall);
                            }
                            else
                            {
                                Navigation.PopModalAsync();
                            }

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }


        }

        private async void showResult()
        {
            await DisplayAlert(Translator.getText("Success"), Translator.getText("UpdateDone"), "Ok");
            await Navigation.PopModalAsync();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {



          
        }


        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            string val = sender.ToString();
        }

        void Handle_Toggled_1(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            throw new NotImplementedException();
        }

       
    }
}
