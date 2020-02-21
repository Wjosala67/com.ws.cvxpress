using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class UnitListRewPage : ContentPage
    {       
        UniListPageRewViewModel viewModel;
        TravelerSpecs IdInfo;

        public UnitListRewPage(TravelerSpecs idInfo)
        {
            InitializeComponent();
            IdInfo = idInfo;
            BindingContext = viewModel = new UniListPageRewViewModel(IdInfo);
            MessagingCenter.Subscribe<UnitUpdatePage, string>(this, "DeletedItem1", async (obj, item) => {

                if (item == "No Content")
                {
                    //await viewModel.onsomecomandAsync();
                    MyListView.ItemsSource = viewModel.LstItemsShow;

                }

            });

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
    async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
            return;
        //RequestSpecs Rs = (RequestSpecs)e.Item;
        //ReserveItemObj Ro = new ReserveItemObj();
        //Ro.requestSpecs = Rs;
        //Ro.travelerSpecs = IdInfo;
        //await Navigation.PushModalAsync(new UnitUpdatePage(Ro));
        //Deselect Item
        ((ListView)sender).SelectedItem = null;
    }


    void Handle_Clicked(object sender, System.EventArgs e)
    {
        throw new NotImplementedException();
    }

    void Handle_Clicked_1(object sender, System.EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    protected override void OnAppearing()
    {
        using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
        {
            Task.Run(async () => {

                await viewModel.onsomecomandAsync();
                MyListView.ItemsSource = viewModel.LstItemsShow;


            }).Wait();

        }

        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        MessagingCenter.Send<UnitListRewPage, string>(this, "UpdateItems", "Done");
        base.OnDisappearing();
    }
}
}
