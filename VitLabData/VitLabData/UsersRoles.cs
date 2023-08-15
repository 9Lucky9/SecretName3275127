namespace VitaLabData
{
    public class UsersRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public UsersRoles(int id, int userId, int roleId)
        {
            Id = id;
            UserId = userId;
            RoleId = roleId;
        }
    }
}
