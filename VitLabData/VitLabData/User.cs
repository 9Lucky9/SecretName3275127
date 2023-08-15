namespace VitLabData
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IEnumerable<UserRoleEnum> Roles { get; set; }

        public User(int userId, string userName, string login, string password)
        {
            Id = userId;
            Name = userName;
            Login = login;
            Password = password;
        }

        public User(int id, string name, string login, string password, IEnumerable<UserRoleEnum> roles)
        {
            Id = id;
            Name = name;
            Login = login;
            Password = password;
            Roles = roles;
        }
    }
}
