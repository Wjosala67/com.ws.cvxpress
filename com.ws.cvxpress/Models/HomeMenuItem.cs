using System;
using System.Collections.Generic;
using System.Text;

namespace com.ws.cvxpress.Models
{
 public enum MenuItemType
    {
        MyProfile,
        OfferBox,
        ListOfferBox,
        ListRequestBox,
        RequestBox,
        Contact,
        Prererences,
        GeneralInfo,
        AboutPage,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }
    }
}
