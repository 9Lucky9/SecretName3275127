namespace VitLabData.DTOs.Create
{
    /// <summary>
    /// Model for creating an user.
    /// </summary>
    public class UserCreateRequest : CreateRequest
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public UserCreateRequest(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }
    }
}
