using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;

using WSViews;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class ChangePassworViewModel : BaseViewModel
    {

        #region Definitios

        public Action DisplayInvalidLoginEmpty;
        public Action DisplayInvalidNoMatchPrompt;
        public Action DisplayInvalidPassPrompt;
        public Action DisplayNoInternet;

        public Users isUser;
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
        private string passwordnew;
        public string PasswordNew
        {
            get { return passwordnew; }
            set
            {
                passwordnew = value;
                OnPropertyChanged();
            }
        }

        private string passwordconfirm;
        public string PasswordConfirm
        {
            get { return passwordconfirm; }
            set
            {
                passwordconfirm = value;
                OnPropertyChanged();
            }
        }
        public ICommand SubmitCommand { protected set; get; }

        #endregion
        public ChangePassworViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        async void OnSubmit()
        {
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    if (password != null && passwordnew != null && passwordconfirm != null)
                    {
                        // if empty show message
                        if (password.Length == 0 && passwordnew.Length == 0 && passwordconfirm.Length == 0)
                        {
                            DisplayInvalidLoginEmpty();
                        }
                        else
                        {


                            Profile profile = new Profile();

                            await getLoginProcess();


                        }
                    }
                    else
                    {

                        DisplayInvalidLoginEmpty();

                    }

                }
                else
                {

                    App.ToastMessage(Translator.getText("NoInternet"), Color.Red);

                }
            }
        }

        private async Task getLoginProcess()
        {
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            Criptografia criptografia = new Criptografia();
            ApiService apiService = new ApiService();
            // go to web service if suceeded go to main

            isUser = await apiService.LoginAsync(profile.Email);


            if (isUser != null)
            {
                string str_in_pass = "";
                if (isUser.Password != "") 
                { 
                     str_in_pass = criptografia.desencriptar(isUser.Password, Constants.AppCode);
                }
                if (password == str_in_pass && passwordnew == passwordconfirm)
                {


                    // get the user to the internal database
                    if (isUser.RegisteredWith == Constants.RegAP)
                    {
                        // save email to properties
                        Application.Current.Properties[Constants.persistUser] = isUser.Email;
                        Application.Current.Properties[Constants.persistPass] = passwordnew;
                        await Application.Current.SavePropertiesAsync();
                        // after login save user to database and redirect to MainPageprofile = new Profile();
                        profile.Email = isUser.Email;
                        profile.FirstName = isUser.FirstName;
                        profile.LastName = isUser.LastName;
                        profile.PhoneNumber = isUser.PhoneNumber;
                        profile.Picture = "";
                        profile.Token = "";
                        profile.userImage = isUser.UserPhoto;
                        profile.Password = criptografia.encryptar(passwordnew, Constants.AppCode);
                    }

                    profile.VerificationCode = isUser.VerificationCode;


                    DatabaseHelper.Update(ref profile, App.Os_Folder);

                   isUser.Password = criptografia.encryptar(passwordnew, Constants.AppCode);

                    await apiService.UpdateUserAsync(profile, 1);

                    MessagingCenter.Send<ChangePassworViewModel, string>(this, "ChgPassMessage", "Done");
                }
                else
                {
                    if(password != str_in_pass) DisplayInvalidPassPrompt();

                    if (passwordnew != passwordconfirm) DisplayInvalidNoMatchPrompt();
                    await Application.Current.SavePropertiesAsync();

                }
            }
            else
            {
                DisplayInvalidLoginEmpty();
            }
        }
    }
}
