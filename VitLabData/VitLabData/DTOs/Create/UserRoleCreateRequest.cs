namespace VitLabData.DTOs.Create
{
    /// <summary>
    /// Model for creating an user role.
    /// </summary>
    public class UserRoleCreateRequest : CreateRequest
    {
        public string Name { get; set; }
        public UserRoleCreateRequest(string name)
        {
            Name = name;
        }
    }
}
