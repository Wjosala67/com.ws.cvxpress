using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;

namespace com.ws.cvxpress.ViewModels
{
    public class RewardPageViewModel : BaseViewModel
    {
        #region Definitions

        private List<RewardObject> lstItemsShow;
        public List<RewardObject> LstItemsShow
        {

            get { return lstItemsShow; }
            set
            {
                if (Equals(value, lstItemsShow)) return;
                lstItemsShow = value;
                OnPropertyChanged(nameof(lstItemsShow));
            }



        }


        #endregion
        public RewardPageViewModel()
        {
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ApiService _apiService = new ApiService();
                Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
                lstItemsShow = new List<RewardObject>();

                lstItemsShow = await _apiService.getUserRewardsAsync(profile.Email);

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
