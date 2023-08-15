using VitLabData.DTOs;
using VitLabData;

namespace VitaLabData.Extensions.Mapper
{
    public static class UserRoleExtensions
    {
        /// <summary>
        /// Convert UserRole to UserRoleDTO format.
        /// </summary>
        public static UserRoleDTO ToDTO(this UserRole userRole)
        {
            return new UserRoleDTO(userRole.Id, userRole.Name);
        }
    }
}
