namespace VitLabData
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserRole(int userRoleId, string roleName)
        {
            Id = userRoleId;
            Name = roleName;
        }
    }
}
