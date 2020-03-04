using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using com.ws.cvxpress.Services;
using Openpay.Xamarin.Abstractions;

namespace com.ws.cvxpress.ViewModels
{
    public class MyCardsPageViewModel : BaseViewModel
    {

        private ObservableCollection<Card> cards;
       
        string Email;
        public MyCardsPageViewModel(string email)
        {
            Email = email;
        }

        public async Task onsomecomandAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
              

                ApiService _apiService = new ApiService();
                cards = new ObservableCollection<Card>();

                List<Card> c = await _apiService.getUserCards(Email);


                foreach (Card item in c)
                {
                    cards.Add(item);
                }


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
