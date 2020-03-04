using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.Views;
using com.ws.cvxpress.Views.RegisterA;
using WSViews;
using WSViews.Classes;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class StepOneViewModel : BaseViewModel
    {
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayInvalidLoginEmpty;
        public Action DisplayNoInternet;
        public Action NoRegisterCall;
        public string pType { get; set; }
        public string Calledfrom { get; set; }
        private Profile profile;
        Criptografia criptografia;

        #region Definitions
        private DateTime passdate;

        

        public DateTime PassDate
        {
            get { return passdate; }
            set
            {
                passdate = value;
                OnPropertyChanged();
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }
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
        private string passport;
        public string Passport
        {
            get { return passport; }
            set
            {
                passport = value;
                OnPropertyChanged();
            }
        }

        private Countries selectedCountry;
        public Countries SelectedCountry
        {
            get { return selectedCountry; }
            set
            {



                if (Equals(value, selectedCountry)) return;
                selectedCountry = value;
                OnPropertyChanged();
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

        private string phonepattern;
        public string PhonePattern
        {
            get { return phonepattern; }
            set
            {
                phonepattern = value;
                OnPropertyChanged();
            }
        }

        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged();
            }
        }


        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged();
            }
        }

        private string flagimage;
        public string FlagImage
        {
            get { return flagimage; }
            set
            {
                flagimage = value;
                OnPropertyChanged();
            }
        }
        private string registeredwith;
        public string RegisteredWith
        {
            get { return registeredwith; }
            set
            {
                registeredwith = value;
                OnPropertyChanged();
            }
        }
        private string countrycode;
        public string CountryCode
        {
            get { return countrycode; }
            set
            {
                countrycode = value;
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
        #endregion




        private ObservableCollection<Countries> lstitemcategories;

        public ObservableCollection<Countries> LstItemCategories
        {
            get { return lstitemcategories; }
            set
            {
                if (Equals(value, lstitemcategories)) return;
                lstitemcategories = value;
                OnPropertyChanged(nameof(lstitemcategories));
            }

        }

        public ICommand SubmitCommand { protected set; get; }
       

        public StepOneViewModel(string calledfrom)
        {
            Calledfrom = calledfrom;
            criptografia = new Criptografia();
            lstitemcategories = new ObservableCollection<Countries>();


             ObservableCollection<Countries> lstc =  new ObservableCollection<Countries>();
            //Get Profile
            profile = DatabaseHelper.GetProfile(App.Os_Folder);
            password = profile.Password;
            email = profile.Email;
            firstName = profile.FirstName;
            lastName = profile.LastName;
            if(profile.PhoneNumber != null && !string.IsNullOrEmpty(profile.PhoneNumber))
            phone = criptografia.desencriptar(profile.PhoneNumber, Constants.AppCode);
            passport = profile.PassportNumber;
            
            code = profile.VerificationCode;
            registeredwith = profile.registeredwith;
            DateTime result;
            passdate = (DateTime.TryParse(profile.PassportDate, out result))? DateTime.Parse(profile.PassportDate): DateTime.Today;
            lstitemcategories = DatabaseHelper.getCountries(App.Os_Folder);

            if (lstitemcategories.Count > 0 && profile.CountryCode != null)
            {
                SelectedCountry = (from c in lstitemcategories
                                   where c.CountryCode == profile.CountryCode
                                   select c).First();
                phonepattern = SelectedCountry.NumberPattern;

                countrycode = ProvideCountries.GetCountryPhoneCode(profile.CountryCode);

                flagimage = profile.CountryCode + ".png";

            int cindx = 0;
            foreach (var item in lstitemcategories)
            {

                if (item.CountryCode == SelectedCountry.CountryCode)
                {

                   index = cindx;
                }
                cindx++;
            }
            }



            SubmitCommand = new Command(OnSubmit);



        }


      

        async void OnSubmit(object obj)
        {
            ApiService apiService = new ApiService();
          
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
                    {
                        // if empty show message
                       
                            DisplayInvalidLoginPrompt();
                       
                    }
                    else
                    {
                        profile = DatabaseHelper.GetProfile(App.Os_Folder);

                        profile.Email = email;
                        profile.FirstName = firstName;
                        profile.LastName = lastName;
                        profile.PassportNumber = "";
                        if(Calledfrom == Constants.RegisterCall) profile.Password = password;
                        profile.PhoneNumber = criptografia.encryptar(phone, Constants.AppCode);
                        profile.Picture = "";
                        
                        profile.VerificationCode = code;
                        
                        profile.PassportDate = PassDate.ToShortDateString();

                        await apiService.UpdateUserAsync(profile, 0);

                        int upd = DatabaseHelper.Update(ref profile, App.Os_Folder);

                        if(upd == 1)
                        {
                            // if the profile is sender, go to mainpage
                            if (Application.Current.Properties.ContainsKey(Constants.UserType))
                            {

                                pType = (string)Application.Current.Properties[Constants.UserType];

                            }
                            if(Calledfrom == Constants.RegisterCall) 
                            { 
                            if(pType == Constants.Traveler || pType == Constants.Sender) 
                            {
                                    if (pType == Constants.Traveler) Application.Current.MainPage = new StepTwoPage(Constants.RegisterCall);
                                    else Application.Current.MainPage = new MainPage();

                                    User_Session user_Session = new User_Session();
                                    user_Session.Email = email;
                                    user_Session.Name = string.Concat(FirstName, " ", LastName);
                                    user_Session.DateSession = DateTime.Now.ToShortDateString();
                                    user_Session.DateSessionEnd = string.Empty;
                                    user_Session.Token = "0";

                                    await apiService.RegisterForTraveler(user_Session);

                            } 
                            }else
                            {
                                NoRegisterCall();
                            }
                        }
                    }

                }
                else
                {

                    App.ToastMessage(Translator.getText("NoInternet"), Color.Red);

                }
            }


        }


    }
}
