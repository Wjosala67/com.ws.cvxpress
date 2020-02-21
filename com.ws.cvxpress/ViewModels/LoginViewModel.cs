using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Views;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.FacebookClient;
using Xamarin.Forms;
using Acr.UserDialogs;
using com.ws.cvxpress.Views.Start;
using com.ws.cvxpress.Services;
using WSViews;
using Microsoft.AppCenter;
//using Microsoft.AppCenter;


namespace com.ws.cvxpress.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayInvalidLoginEmpty;
        public Action DisplayNoInternet;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        string[] permisions = new string[] { "email", "public_profile", "user_posts" };

        #region Definitions

        Criptografia criptografia;
        public bool IsBusy { get; set; } = false;
        public bool IsNotBusy { get { return !IsBusy; } }
        public FacebookProfile Profile { get; set; }

        public Command OnLoginCommand { get; set; }
        public Command OnShareDataCommand { get; set; }
        public Command OnLoadDataCommand { get; set; }
        public Command OnLogoutCommand { get; set; }
        private string Os_Folder { get; set; }

        private bool isloggedin;
        public bool IsLoggedIn
        {
            get { return isloggedin; }
            set
            {
                isloggedin = value;

            }
        }

        public Users isUser;
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
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        public ICommand SubmitCommand { protected set; get; }

        #endregion
       
        public LoginViewModel()
        {
            Os_Folder = App.Os_Folder;
            Profile = new FacebookProfile();
            criptografia = new Criptografia();
            OnLoginCommand = new Command(async () => await LoginAsync());
            OnShareDataCommand = new Command(async () => await ShareDataAsync());
            OnLoadDataCommand = new Command(async () => await LoadData());
            OnLogoutCommand = new Command(() =>
            {
                if (CrossFacebookClient.Current.IsLoggedIn)
                {
                    CrossFacebookClient.Current.Logout();
                    IsLoggedIn = false;
                }
            });






            // if exists, loads the email to the form
            if (Application.Current.Properties.ContainsKey(Constants.persistUser))
            {

                email = (string)Application.Current.Properties[Constants.persistUser];

            }
            if (Application.Current.Properties.ContainsKey(Constants.persistPass))
            {

                password = (string)Application.Current.Properties[Constants.persistPass];

            }
            SubmitCommand = new Command(OnSubmit);



            async void OnSubmit()
            {
                using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                {


                    if (CrossConnectivity.Current.IsConnected)
                        {
                            if (email != null && password != null)
                            {
                                // if empty show message
                                if (email.Length == 0 || password.Length == 0)
                                {
                                    DisplayInvalidLoginPrompt();
                                }
                                else
                                {
                                  

                                    Profile profile = new Profile();

                                    await getLoginProcess(profile, email, password, Constants.RegAP);

                               


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

            async Task LoginAsync()
            {
                using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                {
                    FacebookResponse<bool> response = await CrossFacebookClient.Current.LoginAsync(permisions);
                    switch (response.Status)
                    {
                        case FacebookActionStatus.Completed:
                            IsLoggedIn = true;
                            OnLoadDataCommand.Execute(null);
                            break;
                        case FacebookActionStatus.Canceled:

                            break;
                        case FacebookActionStatus.Unauthorized:
                            await Application.Current.MainPage.DisplayAlert("Unauthorized", response.Message, "Ok");
                            break;
                        case FacebookActionStatus.Error:
                            await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
                            break;
                    }
                }

            }

            async Task ShareDataAsync()
            {
                FacebookShareLinkContent linkContent = new FacebookShareLinkContent("Awesome team of developers, making the world a better place one project or plugin at the time!",
                                                                                    new Uri("http://www.github.com/crossgeeks"));
                var ret = await CrossFacebookClient.Current.ShareAsync(linkContent);
            }

            async Task LoadData()
            {
                using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                {
                   
                    var jsonData = await CrossFacebookClient.Current.RequestUserDataAsync
                (
                      new string[] { "id", "name", "email", "picture", "cover", "friends" }, new string[] { }
                );

                    var data = JObject.Parse(jsonData.Data);
                    Profile = new FacebookProfile()
                    {
                        FullName = data["name"].ToString(),
                        Picture = new UriImageSource { Uri = new Uri($"{data["picture"]["data"]["url"]}") },
                        Email = data["email"].ToString()
                    };

                    Profile profile = new Profile()
                    {
                        FirstName = data["name"].ToString(),
                        Picture = data["picture"]["data"]["url"].ToString(),
                        Email = data["email"].ToString(),
                        Token = Profile.Token

                    };


                    await getLoginProcess(profile, profile.Email, criptografia.encryptar(profile.Email, Constants.AppCode), Constants.RegFB);


                   

                 
                }
            }

        }

        private async Task getLoginProcess(Profile profile, string semail, string spassword, string mode)
        {
            ApiService apiService = new ApiService();
            // go to web service if suceeded go to main

             isUser = await apiService.LoginAsync(semail.ToLower());
             

            if (isUser != null)
            {

                string str_in_pass = criptografia.desencriptar(isUser.Password, Constants.AppCode);

                if (str_in_pass == spassword)
                {



                    // get the user to the internal database
                    if(mode == Constants.RegAP) 
                    {
                        // save email to properties
                        Application.Current.Properties[Constants.persistUser] = email.ToLower();
                        Application.Current.Properties[Constants.persistPass] = spassword;
                        await Application.Current.SavePropertiesAsync();
                        // after login save user to database and redirect to MainPageprofile = new Profile();
                        profile.Email = email.ToLower();
                        profile.FirstName = isUser.FirstName;
                        profile.LastName = isUser.LastName;
                        profile.PhoneNumber = isUser.PhoneNumber;
                        profile.Picture = "";
                        profile.Token = "";
                        profile.userImage = isUser.UserPhoto;
                    }

                    profile.VerificationCode = isUser.VerificationCode;



                    DatabaseHelper.Insert(ref profile, App.Os_Folder);


                    if (isUser.VerificationCode == isUser.ZipCode)
                    {


                        Application.Current.MainPage = new MainPage();
                    }
                    else {

                        Application.Current.MainPage = new VerifyPage();
                    }

                }
                else
                {


                    await Application.Current.SavePropertiesAsync();
                    DisplayInvalidLoginPrompt();
                }

                System.Guid? Token = await AppCenter.GetInstallIdAsync();

                if(isUser.Address.Trim() != Token.ToString())
                {
                    isUser.Address = Token.ToString().Trim();
                    await apiService.UpdateUserAsync(isUser);

                }
            }
            else
            {
                DisplayInvalidLoginPrompt();
            }

           
        }
    }
}




