using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using System.Linq;
using Xamarin.Forms;

namespace com.ws.cvxpress.ChatViews
{
    public class ChatUIViewModel : BaseViewModel
    {

        public ObservableCollection<Travels_Conversations> Messages { get; set; } = new ObservableCollection<Travels_Conversations>();
        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }

        public bool ShowScrollTap { get; set; } = false;
        public bool LastMessageVisible { get; set; } = true;
        public int PendingMessageCount { get; set; } = 0;
        public bool PendingMessageCountVisible { get { return PendingMessageCount > 0; } }

        public int travelID { get; set; }
        public int requesID { get; set; }
        public string traveler { get; set;  }
        public string sender { get; set; }

        public Queue<Travels_Conversations> DelayedMessages { get; set; } = new Queue<Travels_Conversations>();
       
        public ICommand MessageAppearingCommand { get; set; }
        public ICommand MessageDisappearingCommand { get; set; }

        ApiService apiService;

        public ChatUIViewModel()
        {
            apiService = new ApiService();

           
            OnSendCommand = new Command(async () =>
            {
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    Travels_Conversations messag = new Travels_Conversations();
                    messag.Id_tm = Messages.Count;
                    messag.Text = TextToSend;
                    messag.User_Sender = App.User;
                    messag.Id_Travel = travelID;
                    messag.Id_Request = requesID;
                    messag.Image = null;
                    messag.MessDate = DateTime.Now.ToString("dd'-'MM'-'yyyy' 'HH':'mm':'ss");
                    TextToSend = string.Empty;
                   
                    if (!Messages.Contains(messag))
                    {
                       

                        Messages.Add(messag);
                        await apiService.RegisterConvoMessage(messag);

                        //await apiService.PushAsyncGeneral("wjosala67@gmail.com", "Mensaje");
                    }


                    TextToSend = string.Empty;
                }

            });
        }

        public async Task ExecuteLoadItemsCommand()
        {


            Messages.Clear();

            ObservableCollection<Travels_Conversations> MessagesTemp  = new ObservableCollection<Travels_Conversations>();

            MessagesTemp = await apiService.getConvo(travelID, requesID);



           



            if (Messages.Count == 0)
            {
                foreach (Travels_Conversations item in MessagesTemp)
                {
                 
                        Messages.Insert(0, item);
                }
            }
            else
            foreach (Travels_Conversations item in MessagesTemp)
            {
                    var col = (from p in Messages where p.Id_tm == item.Id_tm select p);

                if(col.Count() == 0)
                    {
                        for (int i = 0; i < Messages.Count; i++)
                            Messages.Move(Messages.Count - 1, i);
                        Messages.Insert(0, item);

                    }
                   
            }

            for (int i = 0; i < Messages.Count; i++)
                Messages.Move(Messages.Count - 1, i);


        }

        void OnMessageAppearing(Travels_Conversations message)
        {
            var idx = Messages.IndexOf(message);
            if (idx <= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    while (DelayedMessages.Count > 0)
                    {
                        Messages.Insert(0, message);
                    }
                    ShowScrollTap = false;
                    LastMessageVisible = true;
                    PendingMessageCount = 0;
                });
            }
        }

        void OnMessageDisappearing(Travels_Conversations message)
        {
            var idx = Messages.IndexOf(message);
            if (idx >= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowScrollTap = true;
                    LastMessageVisible = false;
                });

            }
        }


    }
}
