using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using Openpay.Xamarin.Abstractions;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class MyCardsPage : ContentPage
    {
        ApiService _apiService;
        MyCardsPageViewModel viewModel;
        public List<Card> c;
        public MyCardsPage()
        {
            InitializeComponent();
           
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            BindingContext = viewModel = new MyCardsPageViewModel(profile.Email);
            lb_CarDeleteFunc();
            _apiService = new ApiService();

            var t = Task.Run(async () => c = await _apiService.getUserCards(profile.Email));
            t.Wait();

            if (c.Count > 0)
            { 
                CardsList.IsVisible = true;
                CardsList.ItemsSource = c;
                LabelCard.IsVisible = false;
            }
            else
            {
                CardsList.IsVisible = false;
                bt_delete.IsVisible = false;
                LabelCard.IsVisible = true;
            }

            void lb_CarDeleteFunc()
            {
                try
                {
                    bt_delete.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {

                            var delcard = await DisplayAlert(Translator.getText("Notice"), Translator.getText("DeleteCard"), Translator.getText("Yes"), Translator.getText("No"));

                            if (delcard)
                            {
                                _apiService = new ApiService();
                                App.WaitScreenStart(Translator.getText("DeletingCard"));
                                Card selected = (Card)CardsList.SelectedItem;
                                cMessage response = await _apiService.deleteUserCard(profile.Email, selected.Id);
                                App.WaitScreenStop();
                                if (response.RETURN_CODE == 0)
                                {
                                  await  Navigation.PopModalAsync();

                                }
                                else
                                {
                                    await DisplayAlert(Translator.getText("Notice"), response.RETURN_DESCRIPTION, "Ok");
                                    await Navigation.PopModalAsync();
                                }
                            }
                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

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
    }
}
