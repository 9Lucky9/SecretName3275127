namespace VitaLabData.DTOs
{
    public class UserLoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public UserLoginRequest(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
