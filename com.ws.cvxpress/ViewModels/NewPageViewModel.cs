using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class NewPageViewModel : BaseViewModel
    {

        #region Definitions

        public Command LoadItemsCommand { get; set; }
       
        private ObservableCollection<News> news;
        public ObservableCollection<News> News
        {
            get { return news; }
            set {
                news = value;
                OnPropertyChanged();
            }
        }

      
        #endregion

        public NewPageViewModel()
        {

            News = new ObservableCollection<News>();





            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(true);
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                App.WaitScreenStart("LoadingNew");

                ApiService _apiService = new ApiService();
             
                News = await _apiService.getNews();
               

                App.WaitScreenStop();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }
    }


  

}
