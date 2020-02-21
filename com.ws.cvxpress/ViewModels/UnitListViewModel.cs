using System;
using System.Collections.ObjectModel;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;

namespace com.ws.cvxpress.ViewModels
{
    public class UnitListViewModel: BaseViewModel
    {
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

        private ObservableCollection<Item> lstItems;
        public ObservableCollection<Item> LstItems
        {

            get { return lstItems; }
            set
            {
                if (Equals(value, lstItems)) return;
                lstItems = value;
                OnPropertyChanged(nameof(lstItems));
            }



        }

        public UnitListViewModel()
        {
            OnSomeCommand();

            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            if (profile != null)
            {
                if (profile.Picture != "") { UserImage = new Uri(profile.Picture); }
                else
                {
                    UserImage = new Uri("https://images.unsplash.com/photo-1517258922744-606330ad6639?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80");
                }
                name = profile.FirstName + " " + profile.LastName;
                Email = profile.Email;
            }
            else
            {

            }
        }








        private async void OnSomeCommand()
        {
            //ApiService _apiService = new ApiService();
            LstItems = new ObservableCollection<Item>();


            //ObservableCollection<Poll> pollsfilter = await _apiService.GetPollAsync();

            //foreach (Poll item in pollsfilter)
            //{
            //    if (item != null) LstPolls.Add(item);

            //}

            Item item = new Item
            {
                Id = 1,
                Text = "Titulo Box",
                Description = "Ron añejo",
                DepartureDate = DateTime.Now,
                ArrivalDate = DateTime.Now,
                type = "Licores",
                quantity = 2,
                amount = 990.00,
                image = null



            };

            Item item1 = new Item
            {
                Id = 2,
                Text = "Titulo 2 Box",
                Description = "Celular ZTE A1",
                DepartureDate = DateTime.Now,
                ArrivalDate = DateTime.Now,
                type = "Electrónico",
                quantity = 1,
                amount = 990.00,
                image = null



            };


            LstItems.Add(item);
            LstItems.Add(item1);
        }
    }
}
