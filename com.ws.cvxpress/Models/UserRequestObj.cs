using System;
namespace com.ws.cvxpress.Models
{
    public class UserRequestObj
    {
        public int RequestID { get; set; }
        public int status { get; set; }
        public Users UserObject { get; set; }
    }
}
