using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Views.RegisterA;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Start
{
    public partial class SelectProfilePage : ContentPage
    {
        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
           

            Task.Run(async () => {
                Application.Current.Properties[Constants.UserType] = Constants.Traveler;
                await Application.Current.SavePropertiesAsync();

            }).Wait();

            //CHANGE FOR WEB SERVICE INFO COUNTER

            // if the info is complete redirect to main page, 
            //Application.Current.MainPage = new MainPage();
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            if (string.IsNullOrEmpty(profile.FirstName) || string.IsNullOrEmpty(profile.LastName) || string.IsNullOrEmpty(profile.PassportNumber) || string.IsNullOrEmpty(profile.PhoneNumber))
                {
               




                Application.Current.MainPage = new StepOnePage(Constants.RegisterCall);

                }
                else if(!DatabaseHelper.getUserServices()) // change for web service info
                {

                        Application.Current.MainPage = new StepTwoPage(Constants.RegisterCall);
                }
                //else if (!DatabaseHelper.getUserPhotos()) // change for web service info
                //{

                //    Application.Current.MainPage = new StepThreePage(Constants.RegisterCall);
                //}
                else
                {
                    Application.Current.MainPage = new MainPage();
                }

            // if no to stepone of this profile
            //



        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Task.Run(async () => {
                Application.Current.Properties[Constants.UserType] = Constants.Sender;
                await Application.Current.SavePropertiesAsync();

            }).Wait();

            // if the info is complete redirect to main page, 
            //Application.Current.MainPage = new MainPage();
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            if (string.IsNullOrEmpty(profile.FirstName) || string.IsNullOrEmpty(profile.LastName) || string.IsNullOrEmpty(profile.PassportNumber) || string.IsNullOrEmpty(profile.PhoneNumber))
            {





                Application.Current.MainPage = new StepOnePage(Constants.RegisterCall);

            }


            // if no to stepone of this profile
            //

        }

        public SelectProfilePage()
        {
             InitializeComponent();
            //Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            //List<App_Con> lstConf = DatabaseHelper.getConfiguration(App.Os_Folder);
            //App_Con isUserAdmin = lstConf.Find(x => x.Name == "EmailAdmin" && x.Value == profile.Email.Trim());

            //if (isUserAdmin != null)
            //{
            //    bt_traveler.IsVisible = true;
            //}
            //else
            //{
            //    bt_traveler.IsVisible = false;
            //}
        }
    }
}
