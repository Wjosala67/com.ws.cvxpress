using System;
namespace com.ws.cvxpress.Models
{
    public class ConfItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public bool IsSelected { get; set; }
        public ConfItem()
        {
        }
    }
}
