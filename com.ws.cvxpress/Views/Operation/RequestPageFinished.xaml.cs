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
    public partial class RequestPageFinished : ContentPage
    {
        RequestFinishedViewModel viewModel;
        public RequestPageFinished()
        {
            InitializeComponent();
            BindingContext = viewModel = new RequestFinishedViewModel(9);
            NavigationPage.SetHasNavigationBar(this, false);


            MessagingCenter.Subscribe<RequestList, string>(this, "LoadDoneRequests", (obj, item) => {


                using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                {
                    Task.Run(async () => {

                        await viewModel.ExecuteLoadItemsCommand();

                        MyListView.ItemsSource = viewModel.LstItemsShow;


                    }).Wait();

                }


            });

        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            RequestSpecs idInfo = (RequestSpecs)e.Item;
            await Navigation.PushModalAsync(new RequestInfoPage(idInfo, "1"));
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
        protected override void OnAppearing()
        {

           
            base.OnAppearing();
        }
    }
}
