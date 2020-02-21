using System;
namespace com.ws.cvxpress.Models
{
    public class Categories
    {
        public int Cat_Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> Type_Id { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }

        public Categories()
        {
        }
    }
}
