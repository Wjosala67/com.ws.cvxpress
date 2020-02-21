using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.Views.Operation;
using Xamarin.Forms;


namespace com.ws.cvxpress.ViewModels
{
    public class RattingBarViewModal : BaseViewModel
    {

        public ICommand rattingBarCommand { get; set; }
        public ICommand clickCommand { get; set; }
       
        public int Rating { get; set;  }
        private string starimage1;
        public string StarImage1
        {
            get { return starimage1; }
            set
            {
                starimage1 = value;
                OnPropertyChanged();
            }
        }
        private string starimage2;
        public string StarImage2
        {
            get { return starimage2; }
            set
            {
                starimage2 = value;
                OnPropertyChanged();
            }
        }
        private string starimage3;
        public string StarImage3
        {
            get { return starimage3; }
            set
            {
                starimage3 = value;
                OnPropertyChanged();
            }
        }
        private string starimage4;
        public string StarImage4
        {
            get { return starimage4; }
            set
            {
                starimage4 = value;
                OnPropertyChanged();
            }
        }

        private string starimage5;
        public string StarImage5
        {
            get { return starimage5; }
            set
            {
                starimage5 = value;
                OnPropertyChanged();
            }
        }
        private string _selectedStar;
        public string SelectedStar
        {
            get { return _selectedStar; }
            set { _selectedStar = value; OnPropertyChanged();

        }
        }

        private bool selectedstar1;
        private bool selectedstar2;
        private bool selectedstar3;
        private bool selectedstar4;
        private bool selectedstar5;

        private string firstStar;
        public string FirstStar
        {
            get { return firstStar; }
            set
            {
                firstStar = value; OnPropertyChanged();

            }
        }

        private string secondStar;
        public string SecondStar
        {
            get { return secondStar; }
            set
            {
                secondStar = value; OnPropertyChanged();

            }
        }

        private string thirdStar;
        public string ThirdStar
        {
            get { return thirdStar; }
            set
            {
                thirdStar = value; OnPropertyChanged();

            }
        }

        private string fourStar;
        public string FourStar
        {
            get { return fourStar; }
            set
            {
                fourStar = value; OnPropertyChanged();

            }
        }

        private string fiveStar;
        public string FiveStar
        {
            get { return fiveStar; }
            set
            {
                fiveStar = value; OnPropertyChanged();

            }
        }

        private string firstname;
        public string FirstName
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged();
            }
        }
        private string lastname;
        public string LastName
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged();
            }
        }

        internal async Task<string> SendRating(SelectedUser ob, RequestSpecs IdInfo)
        {

            ApiService _apiservice = new ApiService();

            Users_Ratings users_Ratings = new Users_Ratings();

            users_Ratings.Rating = Rating;
            users_Ratings.IdRequest = IdInfo.Id;
            users_Ratings.IdTravel = ob.travelerSpecs.Id;
            users_Ratings.IdTraveler = 0;
            


            string response = await _apiservice.SendRating(users_Ratings);

            return response;
        }

        private string ended;
        public string Ended
        {
            get { return ended; }
            set
            {
                ended = value;
                OnPropertyChanged();
            }
        }

        private string delivered;
        public string Delivered
        {
            get { return delivered; }
            set
            {
                delivered = value;
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
        private bool showclick;
        public bool ShowClick
        {
            get { return showclick; }
            set
            {
                showclick = value;
                OnPropertyChanged();
            }
        }

        private bool showcchat;
        public bool ShowChat
        {
            get { return showcchat; }
            set
            {
                showcchat = value;
                OnPropertyChanged();
            }
        }

        private SelectedUser useraccept;
        public SelectedUser UserAccept
        {
            get { return useraccept; }
            set
            {
                useraccept = value;
                OnPropertyChanged();
            }
        }
     
        private ImageSource image;
        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }
        public RattingBarViewModal(SelectedUser ob)
        {
            FirstName = ob.user.FirstName;
            LastName = ob.user.LastName;
            Image = ob.image;
           
            selectedstar1 = false;
            selectedstar2 = false;
            selectedstar3 = false;
            selectedstar4 = false;
            selectedstar5 = false;
            starimage1 = "emptystar.png";
            starimage2 = "emptystar.png";
            starimage3 = "emptystar.png";
            starimage4 = "emptystar.png";
            starimage5 = "emptystar.png";
            FirstStar = Translator.getText("Bad");
            SecondStar = Translator.getText("Regular");
            ThirdStar = Translator.getText("Good");
            FourStar = Translator.getText("VeryGood");
            FiveStar = Translator.getText("Excellent");

        }

        public void functionimage1()
        {
            if (selectedstar1)
            {

                StarImage1 = "emptystar.png";
                FirstStar = Translator.getText("Bad");
                SecondStar = Translator.getText("Regular");
                ThirdStar = Translator.getText("Good");
                FourStar = Translator.getText("VeryGood");
                FiveStar = Translator.getText("Excellent");
                selectedstar1 = false;
                selectedstar2 = false;
                selectedstar3 = false;
                selectedstar4 = false;
                selectedstar5 = false;
                StarImage2 = "emptystar.png";
                StarImage3 = "emptystar.png";
                StarImage4 = "emptystar.png";
                StarImage5 = "emptystar.png";

            }
            else {
                StarImage1 = "filledstar.png";
                selectedstar1 = true;
                Rating = 1;
                onSelected();
                FirstStar = Translator.getText("Bad");
                SecondStar = Translator.getText("Regular");
                ThirdStar = Translator.getText("Good");
                FourStar = Translator.getText("VeryGood");
                FiveStar = Translator.getText("Excellent");
            }
        }
        public void functionimage2()
        {
            if (selectedstar2)
            {

                StarImage2  = "emptystar.png";

                selectedstar1 = true;
                selectedstar2 = false;
                selectedstar3 = false;
                selectedstar4 = false;
                selectedstar5 = false;

                FirstStar = Translator.getText("Bad");
                SecondStar = Translator.getText("Regular");
                ThirdStar = Translator.getText("Good");
                FourStar = Translator.getText("VeryGood");
                FiveStar = Translator.getText("Excellent");

                StarImage1 = "filledstar.png";

                StarImage3 = "emptystar.png";
                StarImage4 = "emptystar.png";
                StarImage5 = "emptystar.png";

            }
            else
            {
                FirstStar = Translator.getText("Bad");
                SecondStar = Translator.getText("Regular");
                ThirdStar = Translator.getText("Good");
                FourStar = Translator.getText("VeryGood");
                FiveStar = Translator.getText("Excellent");
                Rating = 2;
                onSelected();
                StarImage1 = "filledstar.png";
                StarImage2 = "filledstar.png";
                selectedstar1 = true;
                selectedstar2 = true;

            }
        }
        public void functionimage3()
        {
            if (selectedstar3)
            {
                StarImage1 = "filledstar.png";
                StarImage2 = "filledstar.png";

                selectedstar1 = true;
                selectedstar2 = true;
                selectedstar3 = false;
                selectedstar4 = false;
                selectedstar5 = false;

                FirstStar = Translator.getText("Bad");
                SecondStar = Translator.getText("Regular");
                ThirdStar = Translator.getText("Good");
                FourStar = Translator.getText("VeryGood");
                FiveStar = Translator.getText("Excellent");

                StarImage3 = "emptystar.png";
                StarImage4 = "emptystar.png";
                StarImage5 = "emptystar.png";

            }
            else
            {
                StarImage1 = "filledstar.png";
                StarImage2 = "filledstar.png";
                StarImage3 = "filledstar.png";
                selectedstar1 = true;
                selectedstar2 = true;
                selectedstar3 = true;
                Rating = 3;
                onSelected();
                FirstStar = Translator.getText("Bad");
                SecondStar = Translator.getText("Regular");
                ThirdStar = Translator.getText("Good");
                FourStar = Translator.getText("VeryGood");
                FiveStar = Translator.getText("Excellent");
            }
        }
        public void functionimage4()
        {
            if (selectedstar4)
            {
                StarImage1 = "filledstar.png";
                StarImage2 = "filledstar.png";
                StarImage3 = "filledstar.png";
                StarImage4 = "emptystar.png";
                StarImage5 = "emptystar.png";

                FirstStar = Translator.getText("Bad");
                SecondStar = Translator.getText("Regular");
                ThirdStar = Translator.getText("Good");
                FourStar = Translator.getText("VeryGood");
                FiveStar = Translator.getText("Excellent");


                selectedstar1 = true;
                selectedstar2 = true;
                selectedstar3 = true;
                selectedstar4 = false;
                selectedstar5 = false;


            }
            else
            {
                FirstStar = Translator.getText("Bad");
                SecondStar = Translator.getText("Regular");
                ThirdStar = Translator.getText("Good");
                FourStar = Translator.getText("VeryGood");
                FiveStar = Translator.getText("Excellent");
                Rating = 4;
                onSelected();
                StarImage1 = "filledstar.png";
                StarImage2 = "filledstar.png";
                StarImage3 = "filledstar.png";
                StarImage4 = "filledstar.png";
                selectedstar1 = true;
                selectedstar2 = true;
                selectedstar3 = true;
                selectedstar4 = true;
                selectedstar5 = false;
                StarImage5 = "emptystar.png";
            }
        }
        public void functionimage5()
        {
            if (selectedstar5)
            {
                StarImage1 = "filledstar.png";
                StarImage2 = "filledstar.png";
                StarImage3 = "filledstar.png";
                StarImage4 = "filledstar.png";
                selectedstar1 = true;
                selectedstar2 = true;
                selectedstar3 = true;
                selectedstar4 = true;
                selectedstar5 = false;

                FirstStar = Translator.getText("Bad");
                SecondStar = Translator.getText("Regular");
                ThirdStar = Translator.getText("Good");
                FourStar = Translator.getText("VeryGood");
                FiveStar = Translator.getText("Excellent");


                StarImage5 = "emptystar.png";

            }
            else
            {
                StarImage1 = "filledstar.png";
                StarImage2 = "filledstar.png";
                StarImage3 = "filledstar.png";
                StarImage4 = "filledstar.png";
                StarImage5 = "filledstar.png";
                selectedstar1 = true;
                selectedstar2 = true;
                selectedstar3 = true;
                selectedstar4 = true;
                selectedstar5 = true;
                Rating = 5;
                onSelected();
                FirstStar = Translator.getText("Bad");
                SecondStar = Translator.getText("Regular");
                ThirdStar = Translator.getText("Good");
                FourStar = Translator.getText("VeryGood");
                FiveStar = Translator.getText("Excellent");
            }
        }   


        private void onSelected()
        { 
             MessagingCenter.Send<RattingBarViewModal, string>(this, "RatingMessage", "Rating");
        }

    }

}

