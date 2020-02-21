using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using com.ws.cvxpress.Views.Operation;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class RattingBarViewModel : INotifyPropertyChanged
    {

        public ICommand rattingBarCommand { get; set; }
        public ICommand clickCommand { get; set; }
        public RattingBarViewModel()
        {
            rattingBarCommand = new Command(onItemTapped); // this will execute on tap of image (star)
          
        }

     
        private string _selectedStar;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public string SelectedStar
        {
            get { return _selectedStar; }
            set { _selectedStar = value; NotifyPropertyChanged(); }
        }



        private void onItemTapped(object obj)
        {
            var obje = obj;
            SelectedStar = "Selected Star is " + obj;
        }
    }

}

