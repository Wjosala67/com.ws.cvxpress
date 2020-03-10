using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.Views.Start;
using Newtonsoft.Json.Linq;
using Plugin.FacebookClient;
using WSViews;
using WSViews.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class RegisterPageViewModel: BaseViewModel
    {


        string[] permisions = { "email", "public_profile", "user_posts" };

      
        public bool IsProcessCompleted;
        Criptografia criptografia;
        ApiService _apiService;

        #region Definitions
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
        private string confirmpassword;
        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set
            {
                confirmpassword = value;
                OnPropertyChanged();
            }
        }
       
        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }

        private string selectedState;
        public string SelectedState
        {
            get { return selectedState; }
            set
            {
              


                if (Equals(value, selectedState)) return;
                selectedState = value;
                OnPropertyChanged(nameof(selectedState));
            }
        }


             private string phonecode;
        public string phoneCode
        {
            get { return phonecode; }
            set
            {
                phonecode = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<cCountry> lstCountries;
        public ObservableCollection<cCountry> LstCountries
        {

            get { return lstCountries; }
            set
            {
                if (Equals(value, lstCountries)) return;
                lstCountries = value;
                OnPropertyChanged(nameof(lstCountries
                ));
            }



        }
        private bool isloggedin;
        public bool IsLoggedIn
        {
            get { return isloggedin; }
            set
            {
                isloggedin = value;

            }
        }
        public FacebookProfile Profile { get; set; }
        public Command OnLoginCommand { get; set; }
        public Command OnShareDataCommand { get; set; }
        public Command OnLoadDataCommand { get; set; }
        public Command OnLogoutCommand { get; set; }
        private string Os_Folder { get; set; }
        string countryCode { get; set; }
        public ICommand SubmitCommand { protected set; get; }
        #endregion

        public RegisterPageViewModel()
        {
            _apiService = new ApiService();

            criptografia = new Criptografia();
            Os_Folder = App.Os_Folder;
            Profile = new FacebookProfile();



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


            OnSomeCommand();
            selectedState = "MX";

            SubmitCommand = new Command(OnSubmit);
        }


        public async void OnSomeCommand()
        {


            //var locator = CrossGeolocator.Current;
            //locator.DesiredAccuracy = 50;

            //var position = await locator.GetPositionAsync(new TimeSpan(10000));

            //var country = await locator.GetAddressesForPositionAsync(position, null);


           

            //var address = await locator.GetAddressesForPositionAsync(position, "RJHqIE53Onrqons5CNOx~FrDr3XhjDTyEXEjng-CRoA~Aj69MhNManYUKxo6QcwZ0wmXBtyva0zwuHB04rFYAPf7qqGJ5cHb03RCDw1jIW8l");

            //if (address != null)
            //{

            //    IList<Address> add = address.Cast<Address>().ToList();

            //     countryCode = add.First().CountryCode;

            //}
         


        }

        public async void OnSubmit()
        {



            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmpassword)  )
                {
                    MessagingCenter.Send<RegisterPageViewModel, string>(this, "RegisterMessages", Translator.getText("Error") + "-" + Translator.getText("NoticeRegister"));
                }
                else
                {


                    if(password != confirmpassword)
                    {


                        MessagingCenter.Send<RegisterPageViewModel, string>(this, "RegisterMessages", Translator.getText("Error") + "-" + Translator.getText("NoPasswordMatch"));

                        return;

                    }


                    // 1- Send the information to web service to call registerMethod



                    await RegisterUserAccount(Constants.RegAP);

                  

                    // 3- call the login Method and redirect user to MainPage.


                   

                  

                }

            }
            else
            {

                App.ToastMessage(Translator.getText("NoInternet"), Color.Black, "");
            }
        }

        private async Task RegisterUserAccount(string regType)
        {
            IsProcessCompleted = false;

            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            { 

                var isSuccess = await _apiService.RegisterAsync(Email, Password, regType, Constants.RegisterStep, Constants.UnDone);

                IsProcessCompleted = false;
                

                if (isSuccess == "Created")
                {
                    
                    //MessagingCenter.Send<RegisterPageViewModel, string>(this, "RegisterMessages", Translator.getText("Success") + "-" + Translator.getText("ProfileSuccess"));
                    Application.Current.MainPage = new VerifyPage();

                  

                }
                else if (isSuccess == "Conflict")
                {

                    MessagingCenter.Send<RegisterPageViewModel, string>(this, "RegisterMessages", Translator.getText("Conflict") + "-" + Translator.getText("ProfileExists"));
                }
                else
                {

                    MessagingCenter.Send<RegisterPageViewModel, string>(this, "RegisterMessages", Translator.getText("Error") + "-" + isSuccess);
                }

            }
        }

        async Task LoginAsync()
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

        async Task ShareDataAsync()
        {
            FacebookShareLinkContent linkContent = new FacebookShareLinkContent("Awesome team of developers, making the world a better place one project or plugin at the time!",
                                                                                new Uri("http://www.github.com/crossgeeks"));
            var ret = await CrossFacebookClient.Current.ShareAsync(linkContent);
        }

        async Task LoadData()
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

            // 1- Send the information to web service to call registerMethod

            email = Profile.Email;
            password = criptografia.encryptar(Profile.Email, Constants.AppCode);

            await RegisterUserAccount(Constants.RegFB);


            // 2- when we get the answer save it to database.
            Profile fbProfile = new Profile()
            {
                FirstName = data["name"].ToString(),
                Picture = data["picture"]["data"]["url"].ToString(),
                Email = data["email"].ToString(),
                Token = Profile.Token

            };

           // save to database and then call login method and redirect to MainPage

            DatabaseHelper.Insert(ref fbProfile, App.Os_Folder);

          



        }



    }

   







}

