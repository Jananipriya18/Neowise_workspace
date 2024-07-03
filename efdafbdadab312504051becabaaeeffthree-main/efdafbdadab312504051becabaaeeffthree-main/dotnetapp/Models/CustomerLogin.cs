namespace dotnetapp.Models
{
    public class CustomerLogin
    {
        public string Username { get; set; } 
        public string Password { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
        public string TwoFactorEnabledPassCode { get; set; } 
    }
}
