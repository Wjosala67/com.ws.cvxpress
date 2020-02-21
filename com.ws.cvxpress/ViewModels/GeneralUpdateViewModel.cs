using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class GeneralUpdateViewModel : BaseViewModel
    {
        #region Definitions
        private Color color1;
        public Color bColor1
        {
            get { return color1; }
            set
            {
                color1 = value;
                OnPropertyChanged();
            }
        }

        private Color color2;
        public Color bColor2
        {
            get { return color2; }
            set
            {
                color2 = value;
                OnPropertyChanged();
            }
        }
        private Color color3;
        public Color bColor3
        {
            get { return color3; }
            set
            {
                color3 = value;
                OnPropertyChanged();
            }
        }

        private Color color4;
        public Color bColor4
        {
            get { return color4; }
            set
            {
                color4 = value;
                OnPropertyChanged();
            }
        }
        private Color color5;
        public Color bColor5
        {
            get { return color5; }
            set
            {
                color5 = value;
                OnPropertyChanged();
            }
        }
        private bool level1;
        public bool Level1
        {
            get { return level1; }
            set
            {
                level1 = value;
                OnPropertyChanged();
            }
        }

        private bool level2;
        public bool Level2
        {
            get { return level2; }
            set
            {
                level2 = value;
                OnPropertyChanged();
            }
        }

        private bool level3;
        public bool Level3
        {
            get { return level3; }
            set
            {
                level3 = value;
                OnPropertyChanged();
            }
        }

        private bool level4;
        public bool Level4
        {
            get { return level4; }
            set
            {
                level4 = value;
                OnPropertyChanged();
            }
        }
        private bool level5;
        public bool Level5
        {
            get { return level5; }
            set
            {
                level5 = value;
                OnPropertyChanged();
            }
        }
        private string imagesource1;
        public string ImageSource1
        {
            get { return imagesource1; }
            set
            {
                imagesource1 = value;
                OnPropertyChanged();
            }
        }

        private string imagesource2;
        public string ImageSource2
        {
            get { return imagesource2; }
            set
            {
                imagesource2 = value;
                OnPropertyChanged();
            }
        }
        private string imagesource22;
        public string ImageSource22
        {
            get { return imagesource22; }
            set
            {
                imagesource22 = value;
                OnPropertyChanged();
            }
        }
        private string imagesource3;
        public string ImageSource3
        {
            get { return imagesource3; }
            set
            {
                imagesource3 = value;
                OnPropertyChanged();
            }
        }
        private string imagesource4;
        public string ImageSource4
        {
            get { return imagesource4; }
            set
            {
                imagesource4 = value;
                OnPropertyChanged();
            }
        }
       
        private string from;
        public string From
        {
            get { return from; }
            set
            {
                from = value;
                OnPropertyChanged();
            }
        }
        private string to;
        public string To
        {
            get { return to; }
            set
            {
                to = value;
                OnPropertyChanged();
            }
        }
        private DateTime datefrom;
        public DateTime DateFrom
        {
            get { return datefrom; }
            set
            {
                datefrom = value;
                OnPropertyChanged();
            }
        }

        private DateTime dateto;
        public DateTime DateTo
        {
            get { return dateto; }
            set
            {
                dateto = value;
                OnPropertyChanged();
            }
        }

        private int status;
        public int Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        public ICommand SubmitCommand { protected set; get; }

        TravelerSpecs IdInfo;

        #endregion
        public GeneralUpdateViewModel(TravelerSpecs idInfo)
        {
            IdInfo = idInfo;
            from = idInfo.CountryCodeFrom;
            to = idInfo.CountryCodeTo;
            dateto = idInfo.ToDate;
            DateFrom = idInfo.FromDate;
            status = idInfo.status;

            // if no response from webservice all levels are false
            level1 = false;
            level2 = false;
            level3 = false;
            level4 = false;
            level5 = false;
            color1 = Color.Silver;
            color2 = Color.Silver;
            color3 = Color.Silver;
            color4 = Color.Silver;
            color5 = Color.Silver;
            if (level1 == false) imagesource1 = "baggrey.png";
            if (level2 == false) imagesource2 = "takeoffgrey.png";
            if (level3 == false) imagesource3 = "dcheckgrey.png";
            if (level4 == false) imagesource4 = "landinggrey.png";
            if (level5 == false) imagesource22 = "delayedwhite.png";

            switch(status)
            {
                case 1: 
                    frameOneAction();
                    break;
                case 2:
                    frameTwoAction();
                    break;
                case 3:
                    frameThreeAction();
                    break;
                case 4:
                    frameFourAction();
                    break;
                case 5:
                    frame22Action();
                    break;
            }
            SubmitCommand = new Command(OnSubmit);
        }


        public void frameOneAction()
        {


            if (level1)
            {
                level1 = false;
                ImageSource1 = "baggrey.png";
                bColor1 = Color.Silver;
                status = 0;
                if (level2) { level2 = true; ImageSource2 = "takeoffgrey.png"; bColor2 = Color.Silver; }
                if (level3) { level3 = true; ImageSource3 = "dcheckgrey.png"; bColor3 = Color.Silver; }
                if (level4) { level4 = true; ImageSource4 = "landinggrey.png"; bColor4 = Color.Silver; }
                if (level5) { level5 = true; ImageSource22 = "delayedwhite.png"; bColor5 = Color.Silver; }
            }
            else
            {
                status = 1;
                level1 = true;
                ImageSource1 = "bagorange.png";
                bColor1 = Color.DarkOrange;

            }




        }

        public void frameTwoAction()
        {
            if (level2)
            {
                level2 = false;
                ImageSource2 = "takeoffgrey.png";
                bColor2 = Color.Silver;
                status = 1;
                if (level3) { level3 = true; ImageSource3 = "dcheckgrey.png"; bColor3 = Color.Silver; }
                if (level4) { level4 = true; ImageSource4 = "landinggrey.png"; bColor4 = Color.Silver; }
                if (level5) { level5 = true; ImageSource22 = "delayedwhite.png"; bColor5 = Color.Silver; }
            }
            else
            {
                level2 = true;
                ImageSource2 = "takeofforange.png";
                bColor2 = Color.DarkOrange;
                status = 2;
                level5 = false;
                ImageSource22 = "delayedwhite.png";
                bColor5 = Color.Silver;

                 { level1 = true; ImageSource1 = "bagorange.png"; bColor1 = Color.DarkOrange; }

            }

        }

        internal async Task<int> checkOpenRequests(int id)
        {
            int count = 0;
            App.WaitScreenStart(Translator.getText("Loading"));

            ApiService _apiService = new ApiService();

            count = await _apiService.getOpenRequestsAsync(id);


            App.WaitScreenStop();

            return count;
        }

        public void frame22Action()
        {
            if (level5)
            {
                level5 = false;
                ImageSource22 = "delayedwhite.png";
                bColor5 = Color.Silver;
                status = 2;
                level2 = false;
                ImageSource2 = "takeoffgrey.png";
                bColor2 = Color.Silver;

                { level3 = true; ImageSource3 = "dcheckgrey.png"; bColor3 = Color.Silver; }
                 { level4 = true; ImageSource4 = "landinggrey.png"; bColor4 = Color.Silver; }

               
            }
            else
            {
                level5 = true;
                status = 5;
                ImageSource22 = "delayedorange.png";
                bColor5 = Color.DarkOrange;

                level2 = false;
                ImageSource2 = "takeoffgrey.png";
                bColor2 = Color.Silver;

                { level1 = true; ImageSource1 = "bagorange.png"; bColor1 = Color.DarkOrange; }

                Xamarin.Forms.MessagingCenter.Send<GeneralUpdateViewModel, string>(this, "DelayedMessage", "Delayed");



            }


        }

        public void frameThreeAction()
        {
            if (level3)
            {
                level3 = false;
                ImageSource3 = "dcheckgrey.png";
                bColor3 = Color.Silver;
                status = 3;
                if (level4) { level4 = true; ImageSource4 = "landinggrey.png"; bColor4 = Color.Silver; }
               //if (level5) { level5 = true; ImageSource22 = "delayedwhite.png"; bColor5 = Color.Silver; }
            }
            else
            {
                level3 = true;
                ImageSource3 = "dcheckorange.png";
                bColor3 = Color.DarkOrange;
                status = 3;
                 { level1 = true; ImageSource1 = "bagorange.png"; bColor1 = Color.DarkOrange; }
                if (!level5) 
                { 
                  { level2 = true; ImageSource2 = "takeofforange.png"; bColor2 = Color.DarkOrange; }
                }
                else {
                    { level5 = true; ImageSource22 = "delayedorange.png"; bColor5 = Color.DarkOrange; }
                }
            }

        }

        public void frameFourAction()
        {
            if (level4)
            {
                level4 = false;
                ImageSource4 = "landinggrey.png";
                bColor4 = Color.Silver;
                status = 3;
            }
            else
            {
                level4 = true;
                ImageSource4 = "landingorange.png";
                bColor4 = Color.DarkOrange;
                status = 9;
               { level1 = true; ImageSource1 = "bagorange.png"; bColor1 = Color.DarkOrange; }
                if(!level5)
                { level2 = true; ImageSource2 = "takeofforange.png"; bColor2 = Color.DarkOrange; }
                 { level3 = true; ImageSource3 = "dcheckorange.png"; bColor3 = Color.DarkOrange; }
            }


        }

        async void OnSubmit()
        {
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {


                if (CrossConnectivity.Current.IsConnected)
                {
                    IdInfo.status = status;
                    ApiService _apiservice = new ApiService();
                    IdInfo.FromDate = DateFrom;
                    IdInfo.ToDate = DateTo;
                    string response =  await  _apiservice.UpdateTravelSpecsAsync(IdInfo);

                    if (response == "NoContent")
                    {

                        // create and calculate rewards

                        if(status == 9)
                        {



                            onsomecomandAsync();
                            MessagingCenter.Send<GeneralUpdateViewModel, string>(this, "UpdateTravelInfoClose", "Close");

                        }


                        MessagingCenter.Send<GeneralUpdateViewModel, string>(this, "CloseTravel", "Close");
                      
                     
                    }
                }
                else
                {

                    //DisplayNoInternet();

                }
            }
        }


        public async void  onsomecomandAsync()
        {
            //using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {
          
                ApiService _apiService = new ApiService();
              
                 await _apiService.GetRequestRewardsTravelAsync(IdInfo);



               







            }
        }

        public async void getDeliveredItems()
        {
            //using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {

                ApiService _apiService = new ApiService();

                await _apiService.GetRequestRewardsTravelAsync(IdInfo);


            }
        }
    }
}
