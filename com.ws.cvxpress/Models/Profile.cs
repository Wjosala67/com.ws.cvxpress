using System;
using Xamarin.Forms;
using SQLite;
namespace com.ws.cvxpress.Models
{
    public class Profile
    {
        [PrimaryKey]
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportDate { get; set; }
        public string CountryCode { get; set; }
        public string PassportNumber { get; set; }
        public string Picture { get; set; }
        public string VerificationCode { get; set; }
        public string registeredwith { get; set; }
        public Byte[] userImage { get; set; }



    }
}
