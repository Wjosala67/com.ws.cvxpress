using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Start
{
    public partial class ForgotPage : ContentPage
    {
        public ForgotPage()
        {
            var vm = new ForgotPageViewModel();
            this.BindingContext = vm;

            // DisplayAlert Actions for user notifications, when was not possible, empty info and no internet available
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert(Translator.getText("Error"), Translator.getText("NoticeForgot"), "OK");
            vm.DisplayInvalidLoginEmpty += () => DisplayAlert(Translator.getText("Error"), Translator.getText("NoticeEmptyForgot"), "OK");
            vm.DisplayNoInternet += () => DisplayAlert(Translator.getText("Notice"), Translator.getText("NoInternet"), "OK");
            vm.DisplaySentEmailPrompt += async () =>
            {
                await DisplayAlert(Translator.getText("Notice"), Translator.getText("RecoveryEmailSend"), "OK");
                await Navigation.PopModalAsync();
            };

            InitializeComponent();

            lb_backFunc();
            void lb_backFunc()
            {
                try
                {
                    b_Back.GestureRecognizers.Add(new TapGestureRecognizer()
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
