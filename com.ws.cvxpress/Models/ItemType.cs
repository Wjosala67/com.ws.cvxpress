using System;
namespace com.ws.cvxpress.Models
{
    public class ItemType
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public bool IsSelected { get; set; }
        public string image { get; set; }
        public ItemType()
        {
        }
    }
}
