using System;
namespace com.ws.cvxpress.Models
{
    public class Travels_Conversations
    {

        public int Id_tm { get; set; }
        public int Id_Travel { get; set; }
        public int Id_Request { get; set; }
        public string Text { get; set; }
        public string User_Sender { get; set; }
        public string Image { get; set; }
        public string MessDate { get; set; }

    }
}
