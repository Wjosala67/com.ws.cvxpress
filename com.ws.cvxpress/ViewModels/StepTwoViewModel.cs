using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.Views.RegisterA;
using Xamarin.Forms;
using System.Threading.Tasks;
using com.ws.cvxpress.Views;
using Xamarin.Essentials;

namespace com.ws.cvxpress.ViewModels
{
    public class StepTwoViewModel : BaseViewModel
    {

        #region Definitions
       

        private ObservableCollection<ItemCategories> lstitemcategories;
        public string Calledfrom { get; set; }
        public List<User_Categories> selectedCategories { get; set; }
        public List<User_Types> selectedTypes { get; set; }
        public Action NoRegisterCall;
        public List<User_DeliveryTypes> selectedDelivery { get; set; }
        public List<User_CollectTypes> selectedCollect { get; set; }
        public ObservableCollection<ItemCategories> LstItemCategories
        {
            get { return lstitemcategories; }
            set
            {
                if (Equals(value, lstitemcategories)) return;
                lstitemcategories = value;
                OnPropertyChanged(nameof(lstitemcategories));
            }

        }

        private ObservableCollection<ItemType> lstitemtype;

        public ObservableCollection<ItemType> LstItemType
        {
            get { return lstitemtype; }
            set
            {
                if (Equals(value, lstitemtype)) return;
                lstitemtype = value;
                OnPropertyChanged(nameof(lstitemtype));
            }

        }
        private int categoryheight;
        public int CategoryHeight
        {
            get { return categoryheight; }
            set
            {
                categoryheight = value;
                OnPropertyChanged();
            }
        }

        private int typeheight;
        public int TypeHeight
        {
            get { return typeheight; }
            set
            {
                typeheight = value;
                OnPropertyChanged();
            }
        }


        private int deliveryheight;
        public int DeliveryHeight
        {
            get { return deliveryheight; }
            set
            {
                deliveryheight = value;
                OnPropertyChanged();
            }
        }
        private int collectheight;
        public int CollectHeight
        {
            get { return collectheight; }
            set
            {
                collectheight = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ItemDeliveryTypes> lstdelivertype;

        public ObservableCollection<ItemDeliveryTypes> LstDeliverType
        {
            get { return lstdelivertype; }
            set
            {
                if (Equals(value, lstdelivertype)) return;
                lstdelivertype = value;
                OnPropertyChanged(nameof(lstdelivertype));
            }

        }

        private ObservableCollection<ItemCollectTypes> lstcollecttype;

        public ObservableCollection<ItemCollectTypes> LstCollectType
        {
            get { return lstcollecttype; }
            set
            {
                if (Equals(value, lstcollecttype)) return;
                lstcollecttype = value;
                OnPropertyChanged(nameof(lstcollecttype));
            }

        }


        ListView lstView;
        ListView lstView2;
        ListView lstView3;
        ListView lstView4;
        List<Categories> lstViewA;
        List<Types> lstViewB;
        List<DeliveryTypes> lstViewC;
        List<CollectTypes> lstViewD;
        public List<User_Categories> SavedUserCats;
        public List<User_Types> SavedUserTypes;
        public List<User_DeliveryTypes> SavedUserDelivery;
        public List<User_CollectTypes> SavedUserCollect;

        public ICommand SubmitCommand { protected set; get; }
        #endregion

        public StepTwoViewModel(string calledfrom)
        {

            Task.Run(async () => { await InitializeAsync(); }).Wait();
            Calledfrom = calledfrom;
            lstitemcategories = new ObservableCollection<ItemCategories>();
            lstitemtype = new ObservableCollection<ItemType>();
            lstdelivertype = new ObservableCollection<ItemDeliveryTypes>();
            lstcollecttype = new ObservableCollection<ItemCollectTypes>();

            lstViewA = new List<Categories>();
            lstViewB = new List<Types>();
            lstViewC = new List<DeliveryTypes>(); 
            lstViewD = new List<CollectTypes>(); 
            lstView = new ListView();
            lstView2 = new ListView();
            lstView3 = new ListView();
            lstView4 = new ListView();
            #region switchCell
            lstView.ItemTemplate = new DataTemplate(typeof(SwitchCell));
            lstView.ItemTemplate.SetBinding(SwitchCell.TextProperty, "name");
            lstView.ItemTemplate.SetBinding(SwitchCell.OnProperty, "IsSelected");

            lstView2.ItemTemplate = new DataTemplate(typeof(SwitchCell));
            lstView2.ItemTemplate.SetBinding(SwitchCell.TextProperty, "name");
            lstView2.ItemTemplate.SetBinding(SwitchCell.OnProperty, "IsSelected");

            lstView3.ItemTemplate = new DataTemplate(typeof(SwitchCell));
            lstView3.ItemTemplate.SetBinding(SwitchCell.TextProperty, "name");
            lstView3.ItemTemplate.SetBinding(SwitchCell.OnProperty, "IsSelected");

            lstView4.ItemTemplate = new DataTemplate(typeof(SwitchCell));
            lstView4.ItemTemplate.SetBinding(SwitchCell.TextProperty, "name");
            lstView4.ItemTemplate.SetBinding(SwitchCell.OnProperty, "IsSelected");
            #endregion*/


            lstViewA = DatabaseHelper.getCategoriesApp(App.Os_Folder, 1);
            lstViewB = DatabaseHelper.getTypesApp(App.Os_Folder);
            lstViewC = DatabaseHelper.geDeliveryTypesApp(App.Os_Folder);
            lstViewD = DatabaseHelper.geCollectTypesApp(App.Os_Folder);


            foreach (Categories item in lstViewA)
            {

                int isSelectedint = 0;

                isSelectedint = (from t in SavedUserCats where t.Cat_ID == item.Cat_Id select t.Enabled).FirstOrDefault();

                bool isSelectedBool = (isSelectedint == 1) ? true : false;


                lstitemcategories.Add(

                    new ItemCategories()
                    {
                        Id = item.Cat_Id,
                        ItemName = item.Description,
                        IsSelected = isSelectedBool,
                        image = "item.png"
                    });
            }

            foreach (Types item in lstViewB)
            {

                int isSelectedint = 0;

                isSelectedint = (from t in SavedUserTypes where t.Type_Id == item.Type_Id select t.Enabled).FirstOrDefault();

                bool isSelectedBool = (isSelectedint == 1) ? true : false;

                lstitemtype.Add(
                    new ItemType()
                    {
                        Id = item.Type_Id,
                        ItemName = item.Description,
                        IsSelected = isSelectedBool,
                        image = (item.Type_Id == 1) ? "uno.png" : "dos.png"
                    });
            }

            foreach (DeliveryTypes item in lstViewC)
            {

                int isSelectedint = 0;

                isSelectedint = (from t in SavedUserDelivery where t.Delivery_Id == item.Deli_Type select t.Enabled).FirstOrDefault();

                bool isSelectedBool = (isSelectedint == 1) ? true : false;

                lstdelivertype.Add(
                    new ItemDeliveryTypes()
                    {
                        Id = item.Deli_Type,
                        ItemName = item.Description,
                        IsSelected = isSelectedBool,
                        image = "box.png"
                    });
            }

            foreach (CollectTypes item in lstViewD)
            {

                int isSelectedint = 0;

                isSelectedint = (from t in SavedUserCollect where t.Collect_Id == item.Col_Type select t.Enabled).FirstOrDefault();

                bool isSelectedBool = (isSelectedint == 1) ? true : false;

                lstcollecttype.Add(
                    new ItemCollectTypes()
                    {
                        Id = item.Col_Type,
                        ItemName = item.Description,
                        IsSelected = isSelectedBool,
                        image = "box.png"
                    });
            }


            categoryheight = (lstitemcategories.Count * 43) + 4;

            typeheight = (lstitemtype.Count * 43) + 4;

            deliveryheight = (lstdelivertype.Count * 43) + 4;

            collectheight = (lstcollecttype.Count * 43) + 4;

            SubmitCommand = new Command(OnSubmit);


        }

        public async Task InitializeAsync()
        {
            ApiService apiService = new ApiService();

            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

            SavedUserCats = new List<User_Categories>();
            SavedUserTypes = new List<User_Types>();
            SavedUserDelivery = new List<User_DeliveryTypes>();
            SavedUserCollect = new List<User_CollectTypes>();

            SavedUserCats = await apiService.getUserCategoriesAsync(profile.Email);
            SavedUserTypes = await apiService.getUserTypesAsync(profile.Email);
            SavedUserDelivery = await apiService.getUserDeliveryTypesAsync(profile.Email);
            SavedUserCollect = await apiService.getUserCollectTypesAsync(profile.Email);
        }

        async void OnSubmit(object obj)
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            { 
                using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                {

               

                        ApiService apiService = new ApiService();
                    Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

                    int I = 0;
                    User_Categories ItemSaveCat = new User_Categories();
                    selectedCategories = new List<User_Categories>();
                    selectedTypes = new List<User_Types>();
                    selectedCollect = new List<User_CollectTypes>();
                    selectedDelivery = new List<User_DeliveryTypes>();


                    foreach (ItemCategories item in lstitemcategories)
                    {


                        if (item.IsSelected)
                        {
                            ItemSaveCat = new User_Categories();
                            ItemSaveCat.Email = profile.Email;
                            ItemSaveCat.Cat_ID = item.Id;
                            ItemSaveCat.Enabled = (item.IsSelected) ? 1 : 0;
                            selectedCategories.Add(ItemSaveCat);
                            DatabaseHelper.Insert(ref ItemSaveCat, App.Os_Folder);
                            I++;

                        }


                    }
                    int Y = 0;
                    User_Types ItemSaveTypes = new User_Types();
                    foreach (ItemType item in lstitemtype)
                    {
                        if (item.IsSelected)
                        {
                            ItemSaveTypes = new User_Types();
                            ItemSaveTypes.Email = profile.Email;
                            ItemSaveTypes.Type_Id = item.Id;
                            ItemSaveTypes.Enabled = (item.IsSelected) ? 1 : 0;
                            selectedTypes.Add(ItemSaveTypes);
                            DatabaseHelper.Insert(ref ItemSaveTypes, App.Os_Folder);
                            Y++;

                        }

                    }
                    int Z = 0;
                    User_DeliveryTypes ItemSaveDeliv = new User_DeliveryTypes();
                    foreach (ItemDeliveryTypes item in lstdelivertype)
                    {
                        if (item.IsSelected)
                        {
                            ItemSaveDeliv = new User_DeliveryTypes();
                            ItemSaveDeliv.Email = profile.Email;
                            ItemSaveDeliv.Delivery_Id = item.Id;
                            ItemSaveDeliv.Enabled = (item.IsSelected) ? 1 : 0;
                            selectedDelivery.Add(ItemSaveDeliv);
                            DatabaseHelper.Insert(ref ItemSaveDeliv, App.Os_Folder);
                            Z++;

                        }



                    }
                    int W = 0;
                    User_CollectTypes ItemSaveCollect = new User_CollectTypes();
                    foreach (ItemCollectTypes item in lstcollecttype)
                    {
                        if (item.IsSelected)
                        {
                            ItemSaveCollect = new User_CollectTypes();
                            ItemSaveCollect.Email = profile.Email;
                            ItemSaveCollect.Collect_Id = item.Id;
                            ItemSaveCollect.Enabled = (item.IsSelected) ? 1 : 0;
                            selectedCollect.Add(ItemSaveCollect);
                            DatabaseHelper.Insert(ref ItemSaveCollect, App.Os_Folder);
                            W++;

                        }



                    }

                    if (Y == 0) { MessagingCenter.Send<StepTwoViewModel, string>(this, "StepTwo", Translator.getText("AtLeastType")); return; }
                    if (I == 0) { MessagingCenter.Send<StepTwoViewModel, string>(this, "StepTwo", Translator.getText("AtLeastCategory")); return; }
                    if (Z == 0) { MessagingCenter.Send<StepTwoViewModel, string>(this, "StepTwo", Translator.getText("AtLeastDelivery")); return; }
                    if (W == 0) { MessagingCenter.Send<StepTwoViewModel, string>(this, "StepTwo", Translator.getText("AtLeastCollect")); return; }

                    await apiService.RegisterUserCategoriesAsync(selectedCategories, profile.Email);
                    await apiService.RegisterUserTypesAsync(selectedTypes, profile.Email);
                    await apiService.RegisterUserDeliveryAsync(selectedDelivery, profile.Email);
                    await apiService.RegisterUserCollectAsync(selectedCollect, profile.Email);
                }

                string pType = "";
                if (Application.Current.Properties.ContainsKey(Constants.UserType))
                {

                    pType = (string)Application.Current.Properties[Constants.UserType];

                }
            

               if (Calledfrom == Constants.RegisterCall)
                {
                    if (pType != Constants.Traveler)
                    {
                        Application.Current.MainPage = new MainPage();
                    }else
                    {
                        Application.Current.MainPage = new StepThreePage(Constants.ForIdTrav);
                    }
                }
                else
                {
                    NoRegisterCall();
                }

            }
            else { App.ToastMessage(Translator.getText("NoInternet"), Color.Red, ""); }
        }




    }

   
}
