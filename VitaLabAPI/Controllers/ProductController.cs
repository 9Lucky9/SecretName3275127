using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VitaLabAPI.Exceptions.Products;
using VitaLabAPI.Services.Products;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;
using VitLabData.Extensions.Mapper;

namespace VitaLabAPI.Controllers
{
    [ApiController]
    [Authorize(Policy = IdentityData.ProductManagerPolicyName)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(ProductCreateRequest product)
        {
            try
            {
                await _productService.Create(product);
                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
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
        public async Task<IActionResult> Get(int productId)
        {
            try
            {
                var product = await _productService.GetById(productId);
                return Ok(product.ToDTO());
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [AllowAnonymous]
        [HttpGet("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByPage([FromQuery] int page)
        {
            try
            {
                var products = await _productService.GetByPage(page);
                var productsDto = products.Select(x => x.ToDTO()).ToList();
                return Ok(productsDto);
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
                var products = await _productService.FindByName(name);
                var usersDto = products.Select(x => x.ToDTO());
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
        public async Task<IActionResult> Put(ProductDTO productDTO)
        {
            try
            {
                await _productService.Edit(productDTO);
                return Ok();
            }
            catch (ProductNotFoundException)
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
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                var product = await _productService.GetById(productId);
                await _productService.DeleteById(productId);
                return NoContent();
            }
            catch (ProductNotFoundException)
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
