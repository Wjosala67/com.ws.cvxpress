
using System.Collections.ObjectModel;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class AdjustmentPageViewModel : BaseViewModel
    {
        private ObservableCollection<ConfItem> lstitemtype;

        public ObservableCollection<ConfItem> LstItemType
        {
            get { return lstitemtype; }
            set
            {
                if (Equals(value, lstitemtype)) return;
                lstitemtype = value;
                OnPropertyChanged(nameof(lstitemtype));
            }

        }
        public string pType;
        ListView lstView4;

        public AdjustmentPageViewModel()
        {
            lstView4 = new ListView();
            lstView4.ItemTemplate = new DataTemplate(typeof(SwitchCell));
            lstView4.ItemTemplate.SetBinding(SwitchCell.TextProperty, "name");
            lstView4.ItemTemplate.SetBinding(SwitchCell.OnProperty, "IsSelected");

            if (Application.Current.Properties.ContainsKey(Constants.Notifications))
            {

                pType = (string)Application.Current.Properties[Constants.Notifications];

            }


            LstItemType = new ObservableCollection<ConfItem>();
            LstItemType.Add(
                new ConfItem
                {
                    Id = 1,
                    IsSelected = (pType == Constants.Disabled)? false: true ,
                    ItemName = Translator.getText("Notification")
                });



        }
    }
}
