using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VitaLabAPI.Exceptions.Users;
using VitaLabAPI.Services.UserRoles;
using VitLabData.DTOs.Create;
using VitLabData.DTOs;
using VitaLabAPI.Exceptions.UserRole;
using VitaLabData.DTOs.Create;
using VitaLabData.Extensions.Mapper;

namespace VitaLabAPI.Controllers
{
    [ApiController]
    [Authorize(Policy = IdentityData.AdministratorPolicyName)]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRolesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]UserRoleCreateRequest userCreate)
        {
            try
            {
                await _userRoleService.Create(userCreate);
                return NoContent();
            }
            catch (UserRoleAlreadyExists)
            {
                return BadRequest();
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
        public async Task<IActionResult> Get([FromQuery]int userRoleId)
        {
            try
            {
                var userRole = await _userRoleService.GetById(userRoleId);
                return Ok(userRole);
            }
            catch (UserRoleNotFoundException)
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
                var userRoles = await _userRoleService.GetByPage(page);
                var userRolesDto = userRoles.Select(x => x.ToDTO()).ToList();
                return Ok(userRolesDto);
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]UserRoleDTO userRoleDto)
        {
            try
            {
                await _userRoleService.Edit(userRoleDto);
                return Ok();
            }
            catch (UserRoleNotFoundException)
            {
                return NotFound();
            }
            catch (UserRoleAlreadyExists)
            {
                return BadRequest();
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
        public async Task<IActionResult> Delete(int userRoleId)
        {
            try
            {
                var user = await _userRoleService.GetById(userRoleId);
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

        [HttpPost("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignRoleToUser([FromBody] UsersRolesAssignRequest usersRolesAssignRequest)
        {
            try
            {
                await _userRoleService.AssignRolesToUser(usersRolesAssignRequest);
                return NoContent();
            }
            catch (UserRoleAlreadyExists)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
