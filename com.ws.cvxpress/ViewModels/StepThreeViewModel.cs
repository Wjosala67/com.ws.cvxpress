using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.Views;
using WSViews.Classes;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class StepThreeViewModel : BaseViewModel
    {

        #region Definitions
        public byte[] imagetosave { get; set; }

        public ICommand SubmitCommand { protected set; get; }
        public Action NoRegisterCall;
        public Action VerifyProfile;
        private ObservableCollection<IdInfo> idinfos;
        public int id_document_type { get; set; }
        public ObservableCollection<IdInfo> IdInfos 
        {
        
            get { return idinfos; }
            set
            {
                if (Equals(value, idinfos)) return;
                idinfos = value;
                OnPropertyChanged(nameof(idinfos));
            }

        }


        private ObservableCollection<User_Ident> lstInfo;
        public ObservableCollection<User_Ident> LstInfo
        {

            get { return lstInfo; }
            set
            {
                if (Equals(value, lstInfo)) return;
                lstInfo = value;
                OnPropertyChanged(nameof(lstInfo));
            }

        }
        private string CalledFrom { get; set; } 
        #endregion

        async void OnSubmit(object obj)
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {


                App.WaitScreenStart(Translator.getText("Loading"));
            if (CalledFrom == Constants.RegisterCall)
            {
                Application.Current.MainPage = new MainPage();
            }
            else
            {

                if(CalledFrom == Constants.ForIdPics)
                {
                    ApiService apiService = new ApiService();
                    await apiService.saveUserIdents(idinfos);
                    NoRegisterCall();
                }
                if (CalledFrom == Constants.ForUserPic)
                {
                    ApiService apiService = new ApiService();
                    Profile user = DatabaseHelper.GetProfile(App.Os_Folder);
                    Users userDb = new Users();

                   
                    userDb.Email = user.Email;
                    userDb.FirstName = user.FirstName;
                    userDb.LastName = user.LastName;
                    userDb.Number = user.PhoneNumber;
                    userDb.PassportLimitDate = user.PassportDate;
                    userDb.Password = user.Password;
                    userDb.PhoneNumber = user.PhoneNumber;
                    userDb.RegisteredWith = user.registeredwith;
                    userDb.UserPhoto = user.userImage;
                    userDb.VerificationCode = user.VerificationCode;


                    await apiService.UpdateUserAsync(userDb);
                }
                if(CalledFrom == Constants.ForIdTrav)
                {
                    ApiService apiService = new ApiService();
                    await apiService.saveUserIdents(idinfos);
                    Application.Current.MainPage = new MainPage();
                }

                if (CalledFrom == Constants.ForProductPic)
                {
                    MessagingCenter.Send<StepThreeViewModel, string>(this, "prodpicfinished", "Done");
                }



                App.WaitScreenStop();
               
            }

            }
            else { App.ToastMessage(Translator.getText("NoInternet"), Color.Red); }
        }

        public StepThreeViewModel (string calledfrom) 
        {

            CalledFrom = calledfrom;

            idinfos = new ObservableCollection<IdInfo>();
            lstInfo = new ObservableCollection<User_Ident>();

            if (CalledFrom == Constants.ForIdPics)
            {
               
                    Task.Run(async () => { await InitializeAsync(); }).Wait();

            }

            SubmitCommand = new Command(OnSubmit);


        }

        private async Task InitializeAsync()
        {

                ApiService apiService = new ApiService();

                Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

                lstInfo = await apiService.getUserIdent(profile.Email);

                foreach(User_Ident item in lstInfo)
                {
                    idinfos.Add(new IdInfo
                    {
                        comments = item.Description,
                        Image = ImageManager.BytesToImage(item.Image),
                        
                    });
                }


        }

        public void addIdInfo(IdInfo idInfo)
        {

            idinfos.Add(idInfo);


        }
    }
}
