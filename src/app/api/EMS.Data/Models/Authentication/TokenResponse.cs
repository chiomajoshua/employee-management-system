namespace EMS.Data.Models.Authentication
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}