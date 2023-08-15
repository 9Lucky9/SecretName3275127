using VitLabData.DTOs;

namespace VitLabData.Extensions.Mapper
{
    public static class UserExtensions
    {
        /// <summary>
        /// Convert User to userDTO format.
        /// </summary>
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO(
                user.Id,
                user.Name,
                user.Login,
                user.Password);
        }
    }
}
