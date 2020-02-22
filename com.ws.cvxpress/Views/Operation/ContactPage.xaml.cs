using System;
using System.Collections.Generic;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class ContactPage : ContentPage
    {
        ContactPageViewModel viewModel;
        public ContactPage()
        {
           
            InitializeComponent();
            BindingContext = viewModel = new ContactPageViewModel();
            Title = Translator.getText("Contact");
            // DisplayAlert Actions for user notifications, when was not possible, empty info and no internet available
            viewModel.DisplayInvalidLoginPrompt += () => DisplayAlert(Translator.getText("Error"), Translator.getText("NoticeForgot"), "OK");
            viewModel.DisplayInvalidLoginEmpty += () => DisplayAlert(Translator.getText("Error"), Translator.getText("NoticeEmptyForgot"), "OK");
            viewModel.DisplayNoInternet += () => DisplayAlert(Translator.getText("Notice"), Translator.getText("NoInternet"), "OK");
            viewModel.DisplaySentEmailPrompt += async () =>
            {
                await DisplayAlert(Translator.getText("Notice"), Translator.getText("Created"), "OK");
                Subject.Text = "";
                Topic.SelectedIndex = -1;
            };

            //
            //
            //
            //

            List<string> options = new List<string>();
            options.Add(Translator.getText("ProcessQuestions"));
            options.Add(Translator.getText("AppQuestions"));
            options.Add(Translator.getText("AppComents"));
            options.Add(Translator.getText("IHaveAProblem"));

            Topic.ItemsSource = options;


        }

       

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            viewModel.OnSubmit(Subject.Text);
        }
    }
}
