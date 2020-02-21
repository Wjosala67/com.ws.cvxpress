using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;


namespace com.ws.cvxpress.Views.Operation
{
    public partial class OfferBoxFinished : ContentPage
    {

        OfferBoxFinishedViewModel viewModel;
        public OfferBoxFinished()
        {
            InitializeComponent();
            BindingContext = viewModel = new OfferBoxFinishedViewModel(9);
            NavigationPage.SetHasNavigationBar(this, false);

            MessagingCenter.Subscribe<TravelsList, string>(this, "LoadDoneOffers", (obj, item) => {


                //using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                {
                    Task.Run(async () => {

                        await viewModel.ExecuteLoadItemsCommand();

                        MyListView.ItemsSource = viewModel.LstItemsShow;


                    }).Wait();

                }


            });

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            //TravelerSpecs idInfo = (TravelerSpecs)e.Item;
            //await Navigation.PushAsync(new TravelInfoPage(idInfo));
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnAppearing()
        {

           
            base.OnAppearing();
        }


    }
}
