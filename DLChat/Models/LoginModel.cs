namespace DLChat.Models
{
    public class LoginModel
    {
        public LoginModel(string token, UserModel user)
        {
            this.Token = token;
            this.User = user;
        }
        public UserModel User { get; set; }
        public string Token { get; set; }
    }
}