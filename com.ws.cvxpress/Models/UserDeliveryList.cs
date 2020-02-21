using System;
using System.Collections.Generic;

namespace com.ws.cvxpress.Models
{
    public class UserDeliveryList
    {
        public string email { get; set; }
        public List<User_DeliveryTypes> u_delivery { get; set; }
    }
}
