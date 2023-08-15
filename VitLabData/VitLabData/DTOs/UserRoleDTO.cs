namespace VitLabData.DTOs
{
    public class UserRoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserRoleDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
