using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using com.ws.cvxpress.Views.Operation;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.RegisterA
{
    public partial class TravellerPage : ContentPage
    {
        private string Modal;
        TravellerViewModel viewModel;
      
        public TravellerPage( string modal)
        {
            InitializeComponent();
            Modal = modal;
            BindingContext = viewModel = new TravellerViewModel();
            Title = Translator.getText("OfferBox");
            PickerDeparture.ItemsSource = viewModel.LstitemCountries;
            UnderValidationFrame.IsVisible = false;
            PickerArrival.ItemsSource = viewModel.LstitemCountriesTo;

            viewModel.DisplayInvalidPrompt += async () => {

                var action = await DisplayAlert(Translator.getText("Error"), Translator.getText("Nopicture"), Translator.getText("Yes"), Translator.getText("No"));
                if (action)
                {
                    await Navigation.PushModalAsync(new StepThreePage(Constants.ForIdPics));
                }
            };

            viewModel.DisplayInvalidTravelObject += async () => {

                await DisplayAlert(Translator.getText("Error"), Translator.getText("NoCompleteObjetc"), "Ok");
               

            };
            viewModel.DisplayInvalidDates += async () => {

                await DisplayAlert(Translator.getText("Error"), Translator.getText("InvalidDates"), "Ok");

            };

            viewModel.DisplayInvalidDepArr += async () => {

                await DisplayAlert(Translator.getText("Error"), Translator.getText("InvalidDepArr"), "Ok");

            };

            MessagingCenter.Subscribe<TravellerViewModel, string>(this, "Specs", (obj, item) => {


                if(item == "Done")
                UnderValidationFrame.IsVisible = viewModel.IsTravelerAuth;

            });

            MessagingCenter.Subscribe<TravellerViewModel, string>(this, "SaveSpecs", async (obj, item) =>
            {


                if (item == "Done") 
                { 
                //Definitions.Source = "redlist.png";

                    //var toastConfig = new ToastConfig("Toasting...");
                    //toastConfig.SetDuration(3000);
                    //toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));

                    //UserDialogs.Instance.Toast(toastConfig);

                    await DisplayAlert(Translator.getText("Success"), Translator.getText("OfferBoxCreated"), "Ok");

                    if(Modal == Constants.Modal) 
                    {
                        await Navigation.PopModalAsync();
                        MessagingCenter.Send<TravellerPage, string>(this, "TravelSaved", "Done");
                    }
                    else
                    {

                        App.Current.MainPage = new MainPage { Detail = new NavigationPage(new TravelsList()) };


                        //await Navigation.PushAsync(new TravelsList());
                        //Navigation.RemovePage(this);
                    }
                }

            });
            Label title = new Label();

            title.Text = Translator.getText("TravelAvailability");
            
            title.HorizontalTextAlignment = TextAlignment.Center;
            title.HorizontalOptions = LayoutOptions.Center;

            var size = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            title.FontSize = size;
            if (Modal == Constants.NoModal)
            {
                //GridTop.IsVisible = false;
                //GridGeneral.Children.Add(title, 1, 0);
                GridTop.Children.Add(new Image
                {
                    Source = "logopages.png",
                    HorizontalOptions = LayoutOptions.FillAndExpand

                }, 1, 1);
                LogoImage.IsVisible = false;
                Back.IsVisible = false;
                title.IsVisible = false;

            }
            else
            {
                GridTop.Children.Add(title, 1, 2);
               

            }

            lb_signupFunc();
           
            void lb_signupFunc()
            {
                try
                {
                    LabelDestinyInfo.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            string language = "ES";
                            string terms = Constants.ServerUrl + Constants.ServerUrlPdf + Constants.PdfUSGenerals + language + ".pdf";

                            await Navigation.PushModalAsync(new WSViews.InAppWebView(terms,0));

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }


            lb_BackFunc();
            void lb_BackFunc()
            {
                try
                {
                    Back.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {

                            await Navigation.PopModalAsync();

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

          
        }

        void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Countries s = new Countries();
            if (PickerArrival.SelectedItem != null)
            
            {
                s = (Countries)PickerArrival.SelectedItem;

                if(s.CountryCode == "US") { 
                    LabelDestinyInfo.IsVisible = true;

                }else { LabelDestinyInfo.IsVisible = false; }
            }
        }


        async void Handle_Clicked(object sender, System.EventArgs e)
        {

           if(viewModel.SelectedCountryTo != null) 
           { 
           await Navigation.PushModalAsync(new SpecificsTravelView(viewModel.SelectedCountryTo.CountryCode));
            if (Application.Current.Properties.ContainsKey(Constants.UserType))
            {

               string stravelerSpecs = (string)Application.Current.Properties[Constants.Tspecs];

                if (stravelerSpecs.Contains("|"))
                {
                   viewModel.ImageList = "greenlist.png";
                }
                else
                {
                    viewModel.ImageList = "redlist.png";
                }
            }
            }else {
                await DisplayAlert(Translator.getText("Error"), Translator.getText("selectDestiny"), "Ok");
            }

        }

        protected override void OnAppearing()
        {
          
              
                 viewModel.ExecuteLoadItemsCommand();

              

              

             
            
            base.OnAppearing();
        }
    }
}
