using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using Openpay.Xamarin;
using Openpay.Xamarin.Abstractions;
using WSViews;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class PaymentPageViewModel : BaseViewModel
    {
        #region
        public Action DisplayTravelObjectCreated;
        public Action DisplayTravelObjectNotCreated;
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value  ;
                OnPropertyChanged();
            }
        }
        private string number;

        public Card ExtCard;
        public string Number
        {
            get => number; 
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }
        private int month;
        public int Month
        {
            get { return month; }
            set
            {
                month = value ;
                OnPropertyChanged();
            }
        }
        private int year;
        public int Year
        {
            get { return year; }
            set
            {
                 year = value;
                OnPropertyChanged();
            }
        }

        private string cvv;
        public string CVV
        {
            get { return cvv; }
            set
            {
                  cvv = value;
                OnPropertyChanged();
            }
        }

        private string amount;
        public string Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

        private string line1;
        public string Line1
        {
            get { return line1; }
            set
            {
                line1 = value;
                OnPropertyChanged();
            }
        }

        private string postalcode   ;
        public string PostalCode
        {
            get { return postalcode; }
            set
            {
                postalcode = value;
                OnPropertyChanged();
            }
        }

        private string scity;
        public string SCity
        {
            get { return scity; }
            set
            {
                scity = value;
                OnPropertyChanged();
            }
        }

        private string sstate;
        public string SState
        {
            get { return sstate; }
            set
            {
                sstate = value;
                OnPropertyChanged();
            }
        }

        private string scountry;
        public string SCountry
        {
            get { return scountry; }
            set
            {
                scountry = value;
                OnPropertyChanged();
            }
        }

        public object Crypto { get; private set; }

        private RequestSpecs objRequestSpecs;

        public Command LoadItemsCommand;

        public ObservableCollection<Card> cards;
       
        #endregion

        public PaymentPageViewModel(RequestSpecs requestSpecs)
        {
            name = "";
            number = "";
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            line1 = "";
            postalcode = "";
            scity = "";
            sstate = "";
            scountry = "";
            cvv = "";
            objRequestSpecs = requestSpecs;

            //LoadItemsCommand = new Command(async () => await onsomecomandAsync());
            //LoadItemsCommand.Execute(true);
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

                 List<Card> c = await _apiService.getUserCards(objRequestSpecs.Email);
                

                foreach(Card item in c)
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

        public async void onSubmit(int type)
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                    Criptografia Crypto = new Criptografia();
                    App.WaitScreenStart(Translator.getText("Loading"));

                    Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
                    // save item ObjRequestSpecs
                    ApiService apiService = new ApiService();

                    UInfo uInfo = new UInfo();

                    uInfo.Id = type;
                    uInfo.IdRequest = objRequestSpecs.Id;
                    uInfo.NHold = Crypto.encryptar(Name, Constants.AppCode);



                    uInfo.Email = Crypto.encryptar(profile.Email, Constants.AppCode);
                    uInfo.Token = "";
                    uInfo.V2C = Crypto.encryptar(cvv, Constants.AppCode);
                    var deviceSessionId = string.Empty;
                    if (CrossOpenpay.IsSupported)
                    {
                      deviceSessionId = await CrossOpenpay.Current.CreateDeviceSessionId();
                    }




                    uInfo.coments = "PENDING|" + profile.PhoneNumber + "|" + Crypto.encryptar(month.ToString(), Constants.AppCode) + "|" + Crypto.encryptar(year.ToString(), Constants.AppCode) + "|" + deviceSessionId;

                    if (type == 0)
                    {
                        uInfo.coments = "PENDING|" + profile.PhoneNumber + "|" + Crypto.encryptar(month.ToString(), Constants.AppCode) + "|" + Crypto.encryptar(year.ToString(), Constants.AppCode) + "|" + deviceSessionId + "|" + ExtCard.Id;

                    }
                    else
                    {
                        uInfo.Number = number;
                    }
                    uInfo.Line1 = Crypto.encryptar(Line1, Constants.AppCode);
                    uInfo.PostalCode = Crypto.encryptar(PostalCode, Constants.AppCode);
                    uInfo.sCity = Crypto.encryptar(SCity, Constants.AppCode);
                    uInfo.sState = Crypto.encryptar(SState, Constants.AppCode);


                    uInfo.sCountry = Crypto.encryptar(SCountry, Constants.AppCode);

                    RequestPObject RPO = new RequestPObject();
                    RPO.requestSpecs = objRequestSpecs;
                    RPO.PUInfo = uInfo;

                    string mess = await apiService.RegisterRequestSpecs(RPO);



                    if (mess == "Created")
                    {



                        App.WaitScreenStop();
                        DisplayTravelObjectCreated();
                    }
                    else
                    {
                        App.WaitScreenStop();
                        MessagingCenter.Send<PaymentPageViewModel, string>(this, "CardMessages", mess);
                    }

                    App.WaitScreenStop();

            }
                else { App.ToastMessage(Translator.getText("NoInternet"), Color.Red, ""); }
        }
    }
}
