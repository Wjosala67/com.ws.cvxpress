using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Views.RegisterA;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Openpay.Xamarin.Abstractions;
using System.Threading.Tasks;
using com.ws.cvxpress.Services;
using System.Globalization;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class PaymentPage : ContentPage
    {
        RequestSpecs RequestSpecs;
        PaymentPageViewModel viewModel;

        public List<Card> c;
        ApiService _apiService;
        public PaymentPage(RequestSpecs requestSpecs)
        
        {
            InitializeComponent();
            BindingContext = viewModel = new PaymentPageViewModel(requestSpecs);
            RequestSpecs = requestSpecs;

            string txtAmount = (requestSpecs.ProductValue + requestSpecs.Commission).ToString() + App.strCurrency;
            AmountPay.Text =string.Concat(Translator.getText("NoChargeYet"), " ", Translator.getText("InThatMoment").Replace("{0}", txtAmount));

            ObservableCollection<Countries>  lstcountry = DatabaseHelper.getCountries(App.Os_Folder);

            
            //CardNumber.BorderColor = Color.Green;



            foreach (Countries country in lstcountry)
            {
                Country.Items.Add(country.CountryCode + "-" + country.CountryName);
            }
            Country.SelectedIndex = 0;
            c =  new List<Card>();

            loadCards(RequestSpecs.Email);


            loadPickers();

            viewModel.DisplayTravelObjectCreated += async () =>
            {

                await DisplayAlert(Translator.getText("Success"), Translator.getText("UpdateDone"), "Ok");


                Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new RequestList()) };



            };

            MessagingCenter.Subscribe<PaymentPageViewModel, string>(this, "CardMessages", (obj, item) => {


                DisplayAlert(Translator.getText("Notice"), item, "Ok");


            });

            viewModel.DisplayTravelObjectNotCreated += async () =>
                 {

                     await DisplayAlert(Translator.getText("Error"), Translator.getText("ProcessNotCompleted"), "Ok");


                     



                 };
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

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {

            if (Month.SelectedItem != null && Year.SelectedItem != null)
            {
                if(int.Parse(Month.SelectedItem.ToString()) <= DateTime.Now.Month && int.Parse(Year.SelectedItem.ToString()) <= DateTime.Now.Year)
                {
                    DisplayAlert(Translator.getText("Notice"), Translator.getText("NextMonth"), "OK");
                    return;
                }
                if(TypeEntry.SelectedIndex == 1)
                {
                    if (CardNumber.Text != null)
                    {
                        if(CardNumber.Text.Length != 16)
                        { 
                            DisplayAlert(Translator.getText("Notice"), Translator.getText("CardNumberRequired"), "OK");
                            return;
                        }
                    }
                    else
                    {
                        DisplayAlert(Translator.getText("Notice"), Translator.getText("CardNumberRequired"), "OK");
                        return;
                    }
                }
                else
                {
                    if(c.Count == 0)
                    {
                        DisplayAlert(Translator.getText("Notice"), Translator.getText("NoCardsLoaded"), "OK");
                        return;
                    }
                }
                viewModel.Name = CardHolder.Text;
                if(TypeEntry.SelectedIndex != 0)
                { 
                    if (CardNumber.Text.Length != 16) return;
                    viewModel.Number = CardNumber.Text.Trim(); ;
                }
                viewModel.Year = int.Parse(Year.SelectedItem.ToString());
                viewModel.Month = int.Parse(Month.SelectedItem.ToString());
                viewModel.Line1 = Line1.Text;
                viewModel.PostalCode = PostalCode.Text;
                viewModel.SCity = City.Text;                viewModel.SState = State.Text;
                if (Country.SelectedItem == null)
                {
                    DisplayAlert(Translator.getText("Notice"), Translator.getText("SelectCountry"), "OK");
                    return;
                }
                viewModel.SCountry = Country.SelectedItem.ToString().Split('-')[0];
                viewModel.CVV = CVV.Text;
                if (TypeEntry.SelectedIndex == 0) { viewModel.ExtCard  = (Card)CardsList.SelectedItem; }
                viewModel.onSubmit( TypeEntry.SelectedIndex);

            }
            else
                DisplayAlert(Translator.getText("Notice"), Translator.getText("ValidDate"), "OK");



        }

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        void Handle_SelectedCountryChanged(object sender, System.EventArgs e)
        {

        }
        
        void Handle_SelectedTypeChanged(object sender, System.EventArgs e)
        {

            if (TypeEntry.SelectedItem.ToString() == Translator.getText("existsCard"))
            {
                if (c.Count == 0)
                {
                    DisplayAlert(Translator.getText("Notice"), Translator.getText("NoCardsLoaded"), "Ok");
                    TypeEntry.SelectedIndex = 1;
                    return;
                }
                NewCard.IsVisible = false;
                ExistsCard.IsVisible = true;
               
            }
            else
            {
               
                NewCard.IsVisible = true ;
                ExistsCard.IsVisible = false;
            }
        }

        void Handle_SelectedCardChanged(object sender, System.EventArgs e)
        {
            Card selected = (Card)CardsList.SelectedItem;
            if(selected != null) { 
            CardHolder.Text = selected.HolderName;
            Line1.Text = selected.Address.Line1;
            State.Text = selected.Address.State;
            City.Text = selected.Address.City;
            PostalCode.Text = selected.Address.PostalCode;
            CVV.BorderColor = Color.Green;
            CVV.PlaceholderColor = Color.Green;
            int c = 0;
            foreach(string item in Country.Items)
            {
                if (item.Contains(selected.Address.CountryCode)) Country.SelectedIndex = c;
                c++;
            }
            c = 0;
            foreach (string item in Month.Items)
            {
                if (item.Contains(selected.ExpirationMonth)) Month.SelectedIndex = c;
                c++;
            }
            c = 0;
            foreach (string item in Year.Items)
            {
                if (item.Contains(selected.ExpirationYear)) Year.SelectedIndex = c;
                c++;
            }
            }

        }

        protected override void OnAppearing()
        {
           


          
            base.OnAppearing();
        }

        private void loadCards(string email)
        {
            _apiService = new ApiService();
            var t = Task.Run(async () => c = await _apiService.getUserCards(email));
            t.Wait();

            CardsList.ItemsSource = c;
        }
        private void loadPickers()
        {
            Month.Items.Clear();
            Month.Items.Add("01");
            Month.Items.Add("02");
            Month.Items.Add("03");
            Month.Items.Add("04");
            Month.Items.Add("05");
            Month.Items.Add("06");
            Month.Items.Add("07");
            Month.Items.Add("08");
            Month.Items.Add("09");
            Month.Items.Add("10");
            Month.Items.Add("11");
            Month.Items.Add("12");

            Year.Items.Clear();
           
           

            for (int I = 0; I <= 10; I++)
            {
                Year.Items.Add((DateTime.Now.Year + I).ToString());
            }


            TypeEntry.Items.Clear();
            TypeEntry.Items.Add(Translator.getText("existsCard"));
            TypeEntry.Items.Add(Translator.getText("newCard"));

            TypeEntry.SelectedIndex = 1;
        }

        void CardNumber_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
           
        }
    }
}
