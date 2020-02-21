using System;
using System.Collections.Generic;
using System.Windows.Input;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class ContactPageViewModel : BaseViewModel
    {

        #region Definitions
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayInvalidLoginEmpty;
        public Action DisplayNoInternet;
        public Action DisplaySentEmailPrompt;

        private string topic;
        public string Topic
        {
            get { return topic; }
            set
            {
                topic = value;
                OnPropertyChanged();
            }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                value = text;
                OnPropertyChanged();
            }
        }

        public ICommand SubmitCommand { protected set; get; }
        #endregion
        public ContactPageViewModel()
        {

           
        }

        public async void OnSubmit(string Texto)
        {
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {
                if (CrossConnectivity.Current.IsConnected)
                {


                    if (!string.IsNullOrEmpty(Topic) && !string.IsNullOrEmpty(Texto))
                    {



                        ApiService apiService = new ApiService();
                        //// go to web service if suceeded go to main


                        Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

                        Client_Service cs = new Client_Service();
                        cs.Id = 1;
                        cs.Email = profile.Email;
                        cs.Date = DateTime.Now;
                        cs.status = 0;
                        cs.Text = Texto;
                        cs.Title = topic;

                        string isUser = await apiService.RegisterClientReques(cs);


                        if (isUser == "Created")
                        {
                            DisplaySentEmailPrompt();



                           List<App_Con> listAppCon = DatabaseHelper.getConfiguration(App.Os_Folder);
                            
                            foreach(App_Con item in listAppCon)
                            {
                                if(item.Name == "EmailAdmin")
                                {
                                    // send notificacion 
                                    await apiService.PushAsync(item.Value);
                                }
                            }
                        }
                        else
                        {
                            DisplayInvalidLoginPrompt();
                        }
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
