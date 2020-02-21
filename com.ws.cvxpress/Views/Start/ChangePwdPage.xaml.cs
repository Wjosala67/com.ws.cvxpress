using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Start
{
    public partial class ChangePwdPage : ContentPage
    {
        ChangePassworViewModel viewModel;
        public ChangePwdPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ChangePassworViewModel();
            viewModel.DisplayInvalidLoginEmpty += () => DisplayAlert(Translator.getText("Error"), Translator.getText("NoticeEmptyData"), "OK");
            viewModel.DisplayInvalidNoMatchPrompt += () => DisplayAlert(Translator.getText("Error"), Translator.getText("NoPasswordMatch"), "OK");
            viewModel.DisplayNoInternet += () => DisplayAlert(Translator.getText("Notice"), Translator.getText("NoInternet"), "OK");
            viewModel.DisplayInvalidPassPrompt += () => DisplayAlert(Translator.getText("Notice"), Translator.getText("ActualPassNo"), "OK");

            MessagingCenter.Subscribe<ChangePassworViewModel, string>(this, "ChgPassMessage", (obj, item) => {


                if(item == "Done")
                {
                    Navigation.PopModalAsync();
                }


            });

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



        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
