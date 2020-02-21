using System;
using System.ComponentModel;
using System.Windows.Input;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.Views;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class ForgotPageViewModel : BaseViewModel
    {
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayInvalidLoginEmpty;
        public Action DisplayNoInternet;
        public Action DisplaySentEmailPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public ForgotPageViewModel()
        {

            SubmitCommand = new Command(OnSubmit);
        }

        public async void OnSubmit()
        {
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {
                if (CrossConnectivity.Current.IsConnected)
                {


                    if (email != null)
                    {
                        // if empty shoe message to user
                        if (email.Length == 0)
                        {
                            DisplayInvalidLoginPrompt();
                        }
                        else
                        {



                            ApiService apiService = new ApiService();
                            //// go to web service if suceeded go to main



                            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);




                            var isUser = await apiService.RegisterAsync(email, "", Constants.RegAP, Constants.RecoverStep, Constants.Done);


                            if (isUser != null)
                            {
                                DisplaySentEmailPrompt();

                            }
                            else
                            {
                                DisplayInvalidLoginPrompt();
                            }
                        }
                    }
                    else
                    {
                        DisplayInvalidLoginEmpty();

                    }

                }
                else
                {

                    DisplayNoInternet();

                }
            }
        }
    }
}
