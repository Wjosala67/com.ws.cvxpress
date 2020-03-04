using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.ViewModels;
using com.ws.cvxpress.Views.Operation;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.RegisterA
{
    public partial class RequestPage : ContentPage
    {
        RequestPageViewModel viewModel;
        private string Modal;
        public RequestPage(string modal)
        {
            InitializeComponent();
            Title = Translator.getText("RequestBox");
            Modal = modal;
            BindingContext = viewModel = new RequestPageViewModel();


            PickerDeparture.ItemsSource = viewModel.LstitemCountries;

            PickerArrival.ItemsSource = viewModel.LstitemCountriesTo;

            viewModel.DisplayInvalidTravelObject += async () => {

                await DisplayAlert(Translator.getText("Error"), Translator.getText("NoCompleteObjetc"), "Ok");
                return;
            };

            viewModel.DisplayTravelObjectCreated += async () => {

                await DisplayAlert(Translator.getText("Success"), Translator.getText("UpdateDone"), "Ok");
                if (Modal == Constants.NoModal)
                {

                    Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new RequestList()) };

                }
                else
                {
                   await Navigation.PopModalAsync();

                }

            };

            MessagingCenter.Subscribe<ImagePage, byte[]>(this, "ProductPhoto", (obj, item) => {


                viewModel.ImageByte = item;
                ProductImage.Source = "imagefilledgreen.png";

            });


            MessagingCenter.Subscribe<SpecificsView>(this, "SpecificsView", (obj) => {


               
                //Definitions.Source = "greenlist.png";

            });

            MessagingCenter.Subscribe<RequestPageViewModel>(this, "SendObjectToPayment", async (obj) => {



                await Navigation.PushModalAsync(new PaymentPage(viewModel.requestSpecs));

            });

            Label title = new Label();

            title.Text = Translator.getText("RequestItem");
            title.HorizontalTextAlignment = TextAlignment.Center;
            title.HorizontalOptions = LayoutOptions.Center;

            var size = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            title.FontSize = size;

            if (Modal == Constants.NoModal)
            {
                GridTop.IsVisible = false;
                //GridGeneral.Children.Add(title, 1, 0);
                //LogoImage.IsVisible = false;
                Back.IsVisible = false;
                GridGeneral.Children.Add(title, 1, 0);



                Grid.SetColumnSpan(title, 2);

            }
            else
            {
                GridTop.Children.Add(title, 1, 1);
               
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

            lb_ProdFunc();
            void lb_ProdFunc()
            {
                try
                {
                    ProductImage.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {

                            await Navigation.PushModalAsync(new ImagePage(Constants.ForProductPic));

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {

                if (!string.IsNullOrEmpty(E_ItemQty.Text) && !string.IsNullOrEmpty(E_ItemWeigth.Text) && !string.IsNullOrEmpty(E_ItemValue.Text))
            {

                int peso = int.Parse(Math.Ceiling(decimal.Parse(E_ItemWeigth.Text)).ToString());

                

                if (peso < 0) peso = 1;

                //decimal dectotal = ((decimal.Parse(E_ItemValue.Text) * viewModel.RecoxValorArt / 100) + decimal.Parse(E_ItemWeigth.Text) * viewModel.ValorPorPeso) * int.Parse(E_ItemQty.Text);
                decimal dectotal = Functions.getDeliveryPrice(decimal.Parse(E_ItemValue.Text), peso, int.Parse(E_ItemQty.Text));
                LabelDesc.Text = Translator.getText("DeliveryPrice") + ": " + Math.Round(dectotal, 2).ToString() +  App.strCurrency;
                LabelTotal.Text = Translator.getText("TotalPrice")  + Math.Round(( decimal.Parse(E_ItemValue.Text) * decimal.Parse(E_ItemQty.Text)) + dectotal, 2).ToString() + App.strCurrency ;
                ContinueReq.Text = Translator.getText("ContinueRequest");

            }

               

        }


        async void Handle_Clicked_3(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(E_ItemQty.Text) || string.IsNullOrEmpty(E_ItemName.Text)
                 || string.IsNullOrEmpty(E_ItemFind.Text)
                  || string.IsNullOrEmpty(E_ItemDesc.Text)
                   || string.IsNullOrEmpty(E_ItemValue.Text)
                    || string.IsNullOrEmpty(E_ItemWeigth.Text))

            {

                string mensaje = Translator.getText("Required");

                if (string.IsNullOrEmpty(E_ItemName.Text)) mensaje += Translator.getText("ItemName") + ", ";
                if (string.IsNullOrEmpty(E_ItemDesc.Text)) mensaje += Translator.getText("ItemDesc") + ", ";
                if (string.IsNullOrEmpty(E_ItemFind.Text)) mensaje += Translator.getText("ItemFind").Replace("?","").Replace("¿", "") + ", ";
                decimal result = 0;
                bool valuitem = decimal.TryParse(E_ItemValue.Text, out result);
                if (!valuitem) mensaje += Translator.getText("ProductValue").Replace(":", "") + ", ";
                if (!decimal.TryParse(E_ItemWeigth.Text, out result)) mensaje += Translator.getText("AproxWeight").Replace(":","") + ", ";
                if (!decimal.TryParse(E_ItemQty.Text, out result)) mensaje += Translator.getText("ItemQty") + ", ";

                mensaje = mensaje.Substring(0, mensaje.Length - 2);

                await DisplayAlert(Translator.getText("Notice"), mensaje , "Ok");
                return;



            } 

            string mess = " " + E_ItemQty.Text + " " + E_ItemName.Text;
            string message = Translator.getText("ConfirmReq").Replace("{0}", mess);

           

            var option = await DisplayAlert(Translator.getText("Notice"), message, Translator.getText("Yes"), Translator.getText("No"));

            if (option)
            {
               await viewModel.OnSubmit();
               
            }
        }

        void Handle_CheckedChanged(object sender, System.EventArgs e)
        {
            if(Open.IsChecked) {
                viewModel.OpenDelivery = "Yes";
                viewModel.OpenDays = "No";
                Range.IsChecked = false; 
             }
        }

        void Handle_CheckedChanged_1(object sender, System.EventArgs e)
        {
            if (Range.IsChecked) {
                viewModel.OpenDelivery = "No";
                viewModel.OpenDays = "Yes";
                Open.IsChecked = false; 
            
            }
        }

       

        void Handle_Clicked(object sender, System.EventArgs e)
        {

            Navigation.PushModalAsync(new SpecificsView());
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
