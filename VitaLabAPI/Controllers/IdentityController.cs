using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VitaLabAPI.Exceptions.AuthorizationService;
using VitaLabAPI.Services.AuthorizationServices;
using VitaLabData.DTOs;

namespace VitaLabAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class IdentityController : ControllerBase
    {
        private readonly IVitaLabAuthorizationService _authorizationService;
        private const string LoginErrorMessage = "User is not found, or bad password";

        public IdentityController(IVitaLabAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        [HttpPost("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
        {
            try
            {
                var bearerToken = await _authorizationService.SignIn(userLoginRequest);
                return Ok(bearerToken);
            }
            catch (SignInException)
            {
                return BadRequest(LoginErrorMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpGet("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserInformation(string bearerToken)
        {
            try
            {
                var userDto = await _authorizationService.GetUserInfoByToken(bearerToken);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpGet("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoles(string bearerToken)
        {
            try
            {
                var roles = _authorizationService.GetRoles(bearerToken);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
