using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.Views.Start;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class VerifyPageViewModel : BaseViewModel
    {

        ApiService _apiService;
        public ICommand SubmitCommand { protected set; get; }

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

       

        public VerifyPageViewModel()
        {
            _apiService = new ApiService();
            SubmitCommand = new Command(OnSubmit);
        }


        private async Task VerifyUserAccount()
        {
           

            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {

                Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

                var isSuccess = await _apiService.LoginAsync(profile.Email);

                if (isSuccess != null) { 
                if(isSuccess.VerificationCode == Code)
                {

                    profile.VerificationCode = Code;

                    DatabaseHelper.Update(ref profile, App.Os_Folder);

                    MessagingCenter.Send<VerifyPageViewModel, string>(this, "VerifyMessages", Translator.getText("Success") + "-" + Translator.getText("VerifySuccess"));

                        isSuccess.ZipCode = Code;
                        await _apiService.UpdateUserAsync(isSuccess);
                    
                    Application.Current.MainPage = new SelectProfilePage();
                }
                else
                {

                    MessagingCenter.Send<VerifyPageViewModel, string>(this, "VerifyMessages", Translator.getText("Error") + "-" + Translator.getText("ProfileWrong"));
                }
                } else
                    MessagingCenter.Send<VerifyPageViewModel, string>(this, "VerifyMessages", Translator.getText("Error") + "-" + Translator.getText("ProfileWrong"));

            }
        }

        public async void OnSubmit()
        {



            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {


                    if (string.IsNullOrEmpty(Code))
                    {
                        MessagingCenter.Send<VerifyPageViewModel, string>(this, "VerifyMessages", Translator.getText("Error") + "-" + Translator.getText("NoticeRegister"));
                    }
                    else
                    {








                        await VerifyUserAccount();









                    }

                }
                else
                {

                    MessagingCenter.Send<VerifyPageViewModel, string>(this, "VerifyMessages", Translator.getText("Error") + "-" + Translator.getText("NoInternet"));



                }
            }
        }
    }
}
