using System;
using System.Collections.Generic;
using System.Linq;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        #region Definitions

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

        private string buttontext;
        public string ButtonText
        {
            get { return buttontext; }
            set
            {
                buttontext = value;
                OnPropertyChanged();
            }
        }

        private string titletext;
        public string TitleText
        {
            get { return titletext; }
            set
            {
                titletext = value;
                OnPropertyChanged();
            }
        }

        private string subtitletext;
        public string SubTitleText
        {
            get { return subtitletext; }
            set
            {
                subtitletext = value;
                OnPropertyChanged();
            }
        }

        private List<CarouselObject> myitemsource;
        public List<CarouselObject> myItemsSource
        {
            get { return myitemsource; }
            set
            {
                myitemsource = value;
                OnPropertyChanged();
            }
        }

        private int position;
        public int Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        public string pType { get; set; }

        public ImageSource userImage { get; set; }

        public bool ViewCarrousel { get; set;  }

        public byte[] ImageByte { get; set; }
        #endregion


        public ProfilePageViewModel()
        {
            position = 0;
            string valueAdds;
            string showCarrousel;
            ViewCarrousel = false;
            myItemsSource = new List<CarouselObject>();
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            List<App_Con> AppConList = DatabaseHelper.getConfiguration(App.Os_Folder);

            showCarrousel = (from conf in AppConList where conf.Name == "ShowCarrousel" select conf.Value).FirstOrDefault();

            // if exists, loads the email to the form
            if (Application.Current.Properties.ContainsKey(Constants.UserType))
            {

                pType = (string)Application.Current.Properties[Constants.UserType];

            }
            if (pType == Constants.Traveler)
            {
                buttontext = Translator.getText("OfferBox");
                titletext = Translator.getText("WelcomeTitle").Replace("{APP}", Translator.getText("AppName")); ;
                SubTitleText = Translator.getText("WelcomeSubTitleTraveler").Replace("{button}", buttontext);
               
                if(showCarrousel == "Yes") {
                    ViewCarrousel = true;

                valueAdds = (from conf in AppConList where conf.Name == "TravelersAdds" select conf.Value).FirstOrDefault();
                string title = "";
                for (int i = 1; i <= int.Parse(valueAdds); i++)
                {
                    if (i == 1 && int.Parse(valueAdds) > 1) title = Translator.getText("InfoForYou") + "->";
                    if (i > 1 && i < int.Parse(valueAdds) ) title = "<- "+ Translator.getText("InfoForYou") + " ->";
                    if (i == int.Parse(valueAdds)) title = "<- " + Translator.getText("InfoForYou") + "";
                    myItemsSource.Add(new CarouselObject
                    {
                        Title = title,
                        Image = Constants.ServerUrl + Constants.ServerUrlImages + Constants.Traveler + i + ".gif"

                    });
                }
                }


            }
            else {
                buttontext = Translator.getText("RequestBox");
                titletext = Translator.getText("WelcomeTitle").Replace("{APP}", Translator.getText("AppName"));
                SubTitleText = Translator.getText("WelcomeSubTitleSender").Replace("{button}", buttontext);

                if (showCarrousel == "Yes")
                {
                    ViewCarrousel = true;
                    valueAdds = (from conf in AppConList where conf.Name == "SendersAdds" select conf.Value).FirstOrDefault();

                 // Translator.getText("RequestInstructions");
                string title = "";
                for (int i = 1; i <= int.Parse(valueAdds); i++)
                {
                    if (i == 1 && int.Parse(valueAdds) > 1) title = Translator.getText("InfoForYou") + "->";
                    if (i > 1 && i < int.Parse(valueAdds)) title = "<- " + Translator.getText("InfoForYou") + " ->";
                    if (i == int.Parse(valueAdds)) title = "<- " + Translator.getText("InfoForYou") + "";
                    myItemsSource.Add(new CarouselObject
                    {
                        Title = title,
                        Image = Constants.ServerUrl + Constants.ServerUrlImages + Constants.Sender + i + ".gif"

                    });
                }
                     }

            }


                if (profile != null)
            {
                if (profile.Picture != "") 
                
                {
                    userImage = ImageManager.BytesToImage(profile.userImage);
                } 
                else
                {
                   


                    string pic = (from conf in AppConList where conf.Name == "UserPic" select conf.Description).FirstOrDefault();

                    if (pic != null)
                    UserImage = new Uri(pic);


                   





                }
                name = profile.FirstName; //+ " " + profile.LastName;
                Email = profile.Email;
            }
            else
            {
               
            }
        }
    }
}
