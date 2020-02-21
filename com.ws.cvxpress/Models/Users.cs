using System;
namespace com.ws.cvxpress.Models
{
    public class Users
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Number { get; set; }
        public string VerificationCode { get; set; }
        public string PassportLimitDate { get; set; }
        public byte[] UserPhoto { get; set; }
        public string RegisteredWith { get; set; }
        public string External_id { get; set; }
        public string OptionalCountryCode { get; set; }
        public string OptionalCity { get; set; }
        public string OptionalState { get; set; }
        public string OptionalPostalCode { get; set; }
        public string OptionalAddressLine1 { get; set; }
        public string OptionalAddressLine2 { get; set; }
        public string OptionalAddressLine3 { get; set; }
        public string Token { get; set; }
        public string Optional1 { get; set; }
        public string Optional2 { get; set; }
        public string Optional3 { get; set; }
        public Users()
        {
        }
    }
}
