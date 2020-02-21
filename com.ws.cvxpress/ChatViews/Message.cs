using System;
namespace com.ws.cvxpress.ChatViews
{
    public class Message
    {
        public int Id_tm { get; set; }
        public int Id_Travel { get; set; }
        public int Id_Request { get; set; }
        public string Text { get; set; }
        public string User { get; set; }
        public string Image { get; set; }
    }
}
