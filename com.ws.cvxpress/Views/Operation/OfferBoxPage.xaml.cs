using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class OfferBoxPage : ContentPage
    {
        OfferBoxViewModel viewModel;
        public OfferBoxPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new OfferBoxViewModel(0);
            NavigationPage.SetHasNavigationBar(this, false);


        }

       

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            ShowTravelSpecs showobj = (ShowTravelSpecs)e.Item;

            await Navigation.PushModalAsync(new TravelInfoPage(showobj.travelerSpecs));
            
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnAppearing()
        {
           

                     viewModel.ExecuteLoadItemsCommand();
                    MyListView.ItemsSource = viewModel.LstItemsShow;


            base.OnAppearing();
        }
    }
}
