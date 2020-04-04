using System;
using System.Drawing;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;

namespace com.ws.cvxpress.ViewModels
{
    public class UnitUpdateViewModel : BaseViewModel
    {
        #region Definitions

        ApiService _apiService;
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
        private RequestSpecs updatedrequestspecs;
        public RequestSpecs UpdatedRequestSpecs
        {
            get { return updatedrequestspecs; }
            set {
                updatedrequestspecs = value;
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
            get { return color1; }
            set
            {
                color1 = value;
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
        private decimal newreward;
        public decimal NewReward
        {
            get { return newreward; }
            set
            {
                newreward = value;
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
        private string travelstate;
        public string TravelState
        {
            get { return travelstate; }
            set
            {
                travelstate = value;
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
        private decimal newprice;
        public decimal NewPrice
        {
            get { return newprice; }
            set { newprice = value;  OnPropertyChanged(); }
        }

        private decimal newweight;
        public decimal NewWeight
        {
            get { return newweight; }
            set { newweight = value; OnPropertyChanged(); }
        }
        private decimal oldprice;
        public decimal OldPrice
        {
            get { return oldprice; }
            set { oldprice = value; OnPropertyChanged(); }
        }

        private decimal oldweight;
        public decimal OldWeight
        {
            get { return oldweight; }
            set { oldweight = value; OnPropertyChanged(); }
        }
        #endregion

        ReserveItemObj RIO { get; set; }

        public UnitUpdateViewModel(ReserveItemObj Ro)
        {
            RIO = Ro;
            datefrom = Ro.travelerSpecs.FromDate;
            dateto = Ro.travelerSpecs.ToDate;
            newprice = Ro.requestSpecs.ProductValue;
            newweight = Ro.requestSpecs.Weight;
            oldprice = Ro.requestSpecs.ProductValue;
            oldweight = Ro.requestSpecs.Weight;

            if (Ro.travelerSpecs.status == 3)
            {
                TravelState = Translator.getText("DeliverArticle");
            }else
            {
                TravelState = Translator.getText("NoDeliverArticle");  
            }
            // if no response from webservice all levels are false
            level1 = false;
            level2 = false;
            level3 = false;
            level4 = false;
            color1 = Color.Silver;
            color2 = Color.Silver;
            color3 = Color.Silver;
            color4 = Color.Silver;
            if (level1 == false) imagesource1 = "baggrey.png";
            if (level2 == false) imagesource2 = "takeoffgrey.png";
            if (level3 == false) imagesource3 = "landinggrey.png";
            if (level4 == false) imagesource4 = "dcheckgrey.png";


        }


        public async void UpdateItem()
        {

            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {
                _apiService = new ApiService();

                RIO.requestSpecs.ProductValue = newprice;
                RIO.requestSpecs.Weight = Math.Ceiling(newweight);
                RIO.requestSpecs.Reward = Functions.getReward(newprice, Math.Ceiling(newweight), RIO.requestSpecs.Quantity);
                RIO.requestSpecs.Commission = Functions.getDeliveryPrice(newprice, Math.Ceiling(newweight), RIO.requestSpecs.Quantity);

                if(oldweight != newweight || oldprice != newprice )
                {
                    RIO.requestSpecs.Long = 1;
                }

                if (RIO.requestSpecs.status < Constants.Finished)
                {

                    RIO.requestSpecs.status = Constants.RequestAuth;
                    status = Constants.RequestAuth;
                }
                else RIO.requestSpecs.status = status;

                if (RIO.requestSpecs.status == Constants.Confirmed) RIO.requestSpecs.status = Constants.Finished;
                string sResponse = await _apiService.UpdateItemFTravel(RIO);

                if (sResponse == "NoContent")
                {
                    // Update Lists





                    Xamarin.Forms.MessagingCenter.Send<UnitUpdateViewModel, string>(this, "UnitUpdateFrom", "NoContent");
                }

            }

        }

       
        public void frameFourAction()
        {
            if (level4)
            {
                level4 = false;
                status = RIO.requestSpecs.status;
                ImageSource4 = "dcheckgrey.png";
                bColor4 = Color.Silver;
            }
            else
            {
                level4 = true;
                status = 9;
                ImageSource4 = "dcheckorange.png";
                bColor4 = Color.DarkOrange;
                if (level1 == false) { level1 = true; ImageSource1 = "bagorange.png"; bColor1 = Color.DarkOrange; }
                if (level2 == false) { level2 = true; ImageSource2 = "takeofforange.png"; bColor2 = Color.DarkOrange; }
                if (level3 == false) { level3 = true; ImageSource3 = "landingorange.png"; bColor3 = Color.DarkOrange; }
            }


        }

        public async void DeleteItem()
        {
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {
                ApiService _apiService = new ApiService();

                await _apiService.DeleteItemFTravel(RIO);

            }
        }
    }
}
