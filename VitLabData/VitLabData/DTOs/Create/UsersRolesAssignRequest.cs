namespace VitaLabData.DTOs.Create
{
    public class UsersRolesAssignRequest
    {
        public int UserId { get; set; }
        public IEnumerable<int> RoleIds { get; set; }

        public UsersRolesAssignRequest(int userId, IEnumerable<int> roleIds)
        {
            UserId = userId;
            RoleIds = roleIds;
        }
    }
}
