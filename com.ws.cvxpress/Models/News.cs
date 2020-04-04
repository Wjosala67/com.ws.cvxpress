using System;
namespace com.ws.cvxpress.Models
{
    public class News
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public string Footer { get; set; }
        public int Type { get; set; }
        public int Status { get; set;  }

        public News()
        {
        }
    }
}
