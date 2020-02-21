using System;
using System.Collections.ObjectModel;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using WSViews.Classes;

namespace com.ws.cvxpress.ViewModels
{
    public class DetailAcceptedPageViewModel : BaseViewModel
    {
        #region Definitions
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


        private string started;
        public string Started
        {
            get { return started; }
            set
            {
                started = value;
                OnPropertyChanged();
            }
        }

        private string bought;
        public string Bought
        {
            get { return bought; }
            set
            {
                bought = value;
                OnPropertyChanged();
            }
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

        private ObservableCollection<SelectedUser> lstrequestsAccepted;
        public ObservableCollection<SelectedUser> LstRequestsAccepted
        {

            get { return lstrequestsAccepted; }
            set
            {
                if (Equals(value, lstrequestsAccepted)) return;
                lstrequestsAccepted = value;
                OnPropertyChanged(nameof(lstrequestsAccepted));
            }



        }
        #endregion
        public DetailAcceptedPageViewModel(SelectedUser ob)
        {
            lstrequestsAccepted = new ObservableCollection<SelectedUser>();
            lstrequestsAccepted.Add(ob);



        }
    }
}
