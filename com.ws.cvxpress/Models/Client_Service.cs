using System;
namespace com.ws.cvxpress.Models
{
    public class Client_Service
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Response { get; set; }
        public int status { get; set; }
        public DateTime DateResponse { get; set; }
        public Client_Service()
        {
        }
    }
}
