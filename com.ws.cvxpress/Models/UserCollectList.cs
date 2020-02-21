using System.Collections.Generic;

namespace com.ws.cvxpress.Models
{
    internal class UserCollectList
    {
        public string email { get; set; }
        public List<User_CollectTypes> u_collect { get; set; }
    }
}