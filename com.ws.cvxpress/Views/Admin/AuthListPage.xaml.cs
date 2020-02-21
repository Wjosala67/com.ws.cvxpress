using System;
using System.Collections.Generic;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Admin
{
    public partial class AuthListPage : ContentPage
    {
        AuthListPageViewModel viewModel;
        public AuthListPage()
        {
            BindingContext = viewModel = new AuthListPageViewModel();
            Title = Translator.getText("TravelerAuth");
            InitializeComponent();


            MessagingCenter.Subscribe<AuthListPageViewModel, string>(this, "Auth", (obj, item) => {


                if (item == "Done")
                    MyListView.ItemsSource = viewModel._User_Session;

            });

           
        }


        async void Handle_Clicked_3(object sender, System.EventArgs e)
        {

        }
        async void Handle_Clicked_2(object sender, System.EventArgs e)
        {

        }
        async void Handle_Clicked_4(object sender, System.EventArgs e)
        {

        }

        async void ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {

            if (e.Item == null)
                return;
            User_Session user = (User_Session)e.Item;
            await Navigation.PushModalAsync(new AuthPage(user));


            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            viewModel.ExecuteLoadItemsCommand();

          
            base.OnAppearing();
        }
    }
}
