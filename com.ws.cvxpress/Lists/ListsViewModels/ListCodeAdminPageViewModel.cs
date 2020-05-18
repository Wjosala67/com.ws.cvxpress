using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Lists.Models;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Lists.ListsViewModels
{
    public class ListCodeAdminPageViewModel : BaseViewModel
    {


        #region Definitions
        public Command LoadItemsCommand { get; set; }
        private int vmstatus;

        public int VMStatus
        {
            get { return vmstatus; }
            set
            {
                vmstatus = value;
                OnPropertyChanged();
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
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
        public Uri UserImage { get; set; }

        private ObservableCollection<ListRequestSpecs> lstItems;



        public ObservableCollection<ListRequestSpecs> LstItems
        {

            get { return lstItems; }
            set
            {
                if (Equals(value, lstItems)) return;
                lstItems = value;
                OnPropertyChanged(nameof(lstItems));
            }



        }

        private ObservableCollection<ShowListsSpecs> lstItemsShow;
        public ObservableCollection<ShowListsSpecs> LstItemsShow
        {

            get { return lstItemsShow; }
            set
            {
                if (Equals(value, lstItemsShow)) return;
                lstItemsShow = value;
                OnPropertyChanged(nameof(lstItemsShow));
            }



        }


        public bool ShowImage;


        public ShowListsSpecs sts;
        #endregion


        public ListCodeAdminPageViewModel(int intvmstatus)
        {
            vmstatus = intvmstatus;
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            email = profile.Email;
            //Task.Run(async () => { await InitializeAsync(profile.Email); }).Wait();
            IsBusy = false;
            LoadItemsCommand = new Command(async () => ExecuteLoadItemsCommand());
           


        }


        public async Task InitializeAsync()
        {
            ExecuteLoadItemsCommand();

        }

        public async void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                App.WaitScreenStart(Translator.getText("Loading"));
                ApiService _apiService = new ApiService();
                LstItems = new ObservableCollection<ListRequestSpecs>();
                LstItemsShow = new ObservableCollection<ShowListsSpecs>();

                ObservableCollection<ShowListsSpecs> lsts = new ObservableCollection<ShowListsSpecs>();

               


                LstItems = await _apiService.getTravelRelatedLists(vmstatus);
                foreach (ListRequestSpecs item in LstItems)
                {
                    sts = new ShowListsSpecs();
                    sts.travelerSpecs = new ListRequestSpecs();
                    sts.travelerSpecs = item;
                    sts.imageShow = false;
                    sts.imageSource = "giphy.png";
                    if(item.Status > 0)
                    LstItemsShow.Add(sts);
                }

                MessagingCenter.Send<ListCodeAdminPageViewModel, string>(this, "ListUpdated", "Done");
                IsBusy = false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                App.WaitScreenStop();
                IsBusy = false;
            }

        }
    }
}
