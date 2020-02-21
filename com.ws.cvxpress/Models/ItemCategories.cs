using System;
using Xamarin.Forms;

namespace com.ws.cvxpress.Models
{
    public class ItemCategories
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public bool IsSelected { get; set; }
        public string image { get; set; }

        public ItemCategories()
        {
        }
    }
}
