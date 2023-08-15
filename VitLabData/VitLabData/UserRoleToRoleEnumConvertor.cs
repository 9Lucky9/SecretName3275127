using VitLabData;

namespace VitaLabData
{
    public static class UserRoleToRoleEnumConvertor
    {
        public static UserRoleEnum ConvertToRoleEnum(UserRole userRole)
        {
            switch (userRole.Name)
            {
                case "User":
                    return UserRoleEnum.User;
                case "ProductManager":
                    return UserRoleEnum.ProductManager;
                case "Administrator":
                    return UserRoleEnum.Administrator;
                default:
                    throw new NotImplementedException();
            }
        }
        public static IEnumerable<UserRoleEnum> ConvertToRoleEnum(IEnumerable<UserRole> roles) 
        {
            var rolesList = new List<UserRoleEnum>();
            foreach(UserRole role in roles)
            {
                rolesList.Add(ConvertToRoleEnum(role));
            }
            return rolesList;
        }
    }
}
