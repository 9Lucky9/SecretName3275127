using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VitaLabAPI.Exceptions.AuthorizationService;
using VitaLabAPI.Services.UserRoles;
using VitaLabAPI.Services.Users;
using VitaLabData.DTOs;
using VitLabData;
using VitLabData.DTOs;
using VitLabData.Extensions.Mapper;

namespace VitaLabAPI.Services.AuthorizationServices
{
    /// <inheritdoc cref="IVitaLabAuthorizationService"/>
    public class VitaLabAuthorizationService : IVitaLabAuthorizationService
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _roleService;
        private readonly IConfiguration _configuration;
        private static readonly double BearerTokenLifeTime = 9.0;

        public VitaLabAuthorizationService(IUserService userService, IUserRoleService roleService, IConfiguration configuration)
        {
            _userService = userService;
            _roleService = roleService;
            _configuration = configuration;
        }

        /// <inheritdoc/>
        /// <exception cref="SignInException"></exception>
        public async Task<string> SignIn(UserLoginRequest userLoginRequest)
        {
            var user = await _userService.GetByLogin(userLoginRequest.Login);
            if (user is null)
                throw new SignInException();
            if (user.Password is null)
                throw new SignInException();
            var passwordVerifyResult = new PasswordHasher<UserLoginRequest>()
                .VerifyHashedPassword(
                userLoginRequest,
                user.Password,
                userLoginRequest.Password);
            if(passwordVerifyResult is PasswordVerificationResult.Failed)
                throw new SignInException();
            return CreateBearerToken(user);
        }

        private string CreateBearerToken(User user)
        {
            var claims = GetValidClaims(user);

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration.GetSection("Tokens:AuthToken").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Tokens:Issuer").Value,
                audience: _configuration.GetSection("Tokens:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddHours(BearerTokenLifeTime),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }

        public async Task<UserDTO> GetUserInfoByToken(string bearerToken)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(bearerToken);
            var userId = jwt.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier);
            var user = await _userService.GetById(Convert.ToInt32(userId.First().Value));
            return user.ToDTO();
        }

        /// <inheritdoc/>
        public IEnumerable<UserRoleEnum> GetRoles(string bearerToken)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(bearerToken);
            var userRoles = new List<UserRoleEnum>();
            var roleClaims = jwt.Claims.Where(x => x.Type == ClaimTypes.Role);
            foreach (var roleClaim in roleClaims)
            {
                var role = Enum.Parse<UserRoleEnum>(roleClaim.Value);
                userRoles.Add(role);
            }
            return userRoles;
        }

        private List<Claim> GetValidClaims(User user)
        {
            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, user.Name)
            };
            foreach (var userRole in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
            }
            return claims;
        }
    }
}
