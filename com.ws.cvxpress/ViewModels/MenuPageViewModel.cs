using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using com.ws.cvxpress.Models;

namespace com.ws.cvxpress.ViewModels
{
    public class MenuPageViewModel : INotifyPropertyChanged
    {
        ObservableCollection<HomeMenuItem> itemlist;
        public ObservableCollection<HomeMenuItem> ItemList
        {
            get
            {
                return itemlist;
            }
            set
            {
                itemlist = value;
                OnPropertyChanged("ItemList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null) return;
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
           [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;

        }
        public MenuPageViewModel()
        {
            ItemList = new ObservableCollection<HomeMenuItem>();
        }
    }
}
