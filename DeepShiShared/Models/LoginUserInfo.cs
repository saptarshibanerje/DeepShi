namespace DeepShiShared.Models
{
    public class LoginUserInfo
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
        //public string AuthToken { get; set; }
        public string LoginMode { get; set; }
        public bool IsRememberMe { get; set; }
    } 
}
