using System;
namespace com.ws.cvxpress.Models
{
    public class ItemDeliveryTypes
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public bool IsSelected { get; set; }
        public string image { get; set; }
        public ItemDeliveryTypes()
        {
        }
    }
}
