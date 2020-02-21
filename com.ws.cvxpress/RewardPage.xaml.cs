using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using com.ws.cvxpress.Views.Operation;
using Xamarin.Forms;

namespace com.ws.cvxpress
{
    public partial class RewardPage : ContentPage
    {
        RewardPageViewModel viewModel;
        public RewardPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new RewardPageViewModel();

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

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            TravelerSpecs tspecs = new TravelerSpecs();
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {

                if (e.Item == null)
                    return;

               
                Task.Run(async () => {

                    RewardObject pl = (RewardObject)e.Item;

                   

                    ApiService _apiservice = new ApiService();

                   

                    tspecs = await _apiservice.GetTravelSpecsAsync(pl.IdTravel);

                   

                }).Wait();

            }

             Navigation.PushModalAsync(new UnitListRewPage(tspecs));
        }

      

        protected override void OnAppearing()
        {
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {
                Task.Run(async () => {

                    await viewModel.ExecuteLoadItemsCommand();
                    MyListView.ItemsSource = viewModel.LstItemsShow;


                }).Wait();

            }
            base.OnAppearing();
        }
    }
}
