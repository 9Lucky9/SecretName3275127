using System.Text.Json.Serialization;

namespace VitLabData.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IEnumerable<UserRoleDTO> Roles { get; set; }

        public UserDTO(int id, string name, string login, string password)
        {
            Id = id;
            Name = name;
            Login = login;
            Password = password;
        }

        [JsonConstructor]
        public UserDTO(int id, string name, string login, string password, IEnumerable<UserRoleDTO> roles)
        {
            Id = id;
            Name = name;
            Login = login;
            Password = password;
            Roles = roles;
        }
    }
}
