using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VitaLabAPI.Exceptions.Users;
using VitaLabAPI.Services.Users;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;
using VitLabData.Extensions.Mapper;

namespace VitaLabAPI.Controllers
{
    [ApiController]
    [Authorize(Policy = IdentityData.AdministratorPolicyName)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        [HttpPost("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(UserCreateRequest userCreate)
        {
            try
            {
                await _userService.Create(userCreate);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message} at {GetType().Name}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery]int userId)
        {
            try
            {
                var user = await _userService.GetById(userId);
                return Ok(user.ToDTO());
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByPage([FromQuery] int page)
        {
            try
            {
                var users = await _userService.GetByPage(page);
                var usersDto = users.Select(x => x.ToDTO()).ToList();
                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByLogin([FromQuery]string login)
        {
            try
            {
                var user = await _userService.GetByLogin(login);
                return Ok(user.ToDTO());
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FindByName([FromQuery] string name)
        {
            try
            {
                var users = await _userService.FindByName(name);
                var usersDto = users.Select(x => x.ToDTO());
                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(UserDTO userDto)
        {
            try
            {
                await _userService.Edit(userDto);
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                var user = await _userService.GetById(userId);
                return NoContent();
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
