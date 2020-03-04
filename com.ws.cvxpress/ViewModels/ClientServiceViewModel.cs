using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class ClientServiceViewModel: BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }
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
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Client_Service> lstItems;
        public ObservableCollection<Client_Service> LstItems
        {

            get { return lstItems; }
            set
            {
                if (Equals(value, lstItems)) return;
                lstItems = value;
                OnPropertyChanged(nameof(lstItems));
            }



        }
        public ClientServiceViewModel()
        {
            lstItems = new ObservableCollection<Client_Service>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());

        }

        private async Task ExecuteLoadItems()
        {
            if (IsBusy)
                return;

                IsBusy = true;

                try
                {
                    ApiService _apiService = new ApiService();
                    lstItems = new ObservableCollection<Client_Service>();


                    lstItems = await _apiService.GetClientsQuestionsAsync();
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

        public async Task InitializeAsync(string email)
        {
            {

                ApiService _apiService = new ApiService();
                lstItems = new ObservableCollection<Client_Service>();


                lstItems = await _apiService.GetClientsQuestionsAsync();

               
            }

        }
    }

}
