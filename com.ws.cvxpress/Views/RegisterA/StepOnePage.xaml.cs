using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using com.ws.cvxpress.Views.Start;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.RegisterA
{
    public partial class StepOnePage : ContentPage
    {
        private StepOneViewModel viewmodel;
        private string CalledFrom { get; set; }

        public StepOnePage(string calledFrom)
        {
            InitializeComponent();
            CalledFrom = calledFrom;
            BindingContext = viewmodel = new StepOneViewModel(CalledFrom);
            string pType = "";
            if (Application.Current.Properties.ContainsKey(Constants.UserType))
            {

                pType = (string)Application.Current.Properties[Constants.UserType];

            }
            if (pType == Constants.Traveler)
            {
                
            }
            else
            {
                Lb_Doc.IsVisible = false;
                DocDate.IsVisible = false;
                DocDate.Date = Convert.ToDateTime("01/01/1900");
            }

                // DisplayAlert Actions for user notifications, when was not possible, empty info and no internet available
                viewmodel.DisplayInvalidLoginPrompt += () => DisplayAlert(Translator.getText("Error"), Translator.getText("NoticeEmptyRegister"), "OK");
            viewmodel.DisplayInvalidLoginEmpty += () => DisplayAlert(Translator.getText("Error"), Translator.getText("NoticeEmptyData"), "OK");
            viewmodel.DisplayNoInternet += () => DisplayAlert(Translator.getText("Notice"), Translator.getText("NoInternet"), "OK");
            viewmodel.NoRegisterCall += () => showResult(); 



            PickerCountry.ItemsSource = viewmodel.LstItemCategories;

           


             PickerCountry.SelectedIndex = viewmodel.Index;

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
                                Application.Current.MainPage = new SelectProfilePage();
                            }
                            else
                            {
                                await Navigation.PopModalAsync();
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

       

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (PickerCountry.SelectedIndex != viewmodel.Index)
            {
                E_Phone.Text = "";
               
            }
            Countries customPicker = viewmodel.SelectedCountry;

            viewmodel.FlagImage = customPicker.CountryCode + ".png";

            viewmodel.CountryCode = "( +" + ProvideCountries.GetCountryPhoneCode(customPicker.CountryCode) + " )";

            WSViews.MaskedBehavior mn = new WSViews.MaskedBehavior();

            mn.Mask = customPicker.NumberPattern;
            E_Phone.Behaviors.Clear();
            E_Phone.Behaviors.Add(mn);

        }
    }
}
