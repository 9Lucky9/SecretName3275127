using Microsoft.AspNetCore.Mvc;
using VitaLabAPI.Exceptions.OrderDatas;
using VitaLabAPI.Exceptions.Orders;
using VitaLabAPI.Exceptions.Products;
using VitaLabAPI.Services.OrderDatas;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;
using VitLabData.Extensions.Mapper;

namespace VitaLabAPI.Controllers
{
    [ApiController]
    public class OrderDataController : ControllerBase
    {
        private readonly IOrderDataService _orderDataService;

        public OrderDataController(IOrderDataService orderDataService)
        {
            _orderDataService = orderDataService;
        }

        [HttpPost("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(OrderDataCreateRequest orderDataCreate)
        {
            try
            {
                await _orderDataService.Create(orderDataCreate);
                return Ok();
            }
            catch (OrderNotFoundException)
            {
                return BadRequest($"Order with id {orderDataCreate.OrderId} is not found.");
            }
            catch (ProductNotFoundException)
            {
                return BadRequest($"Product with id {orderDataCreate.ProducId} is not found.");

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
        public async Task<IActionResult> Get(int orderDataId)
        {
            try
            {
                var orderData = await _orderDataService.GetById(orderDataId);
                return Ok(orderData.ToDTO());
            }
            catch (OrderDataNotFoundException)
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            try
            {
                var orderData = await _orderDataService.GetOrderDatasByOrderId(orderId);
                return Ok(orderData.Select(x => x.ToDTO()));
            }
            catch (OrderNotFoundException)
            {
                return BadRequest();
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
        public async Task<IActionResult> Put(OrderDataDTO orderDataDTO)
        {
            try
            {
                await _orderDataService.Edit(orderDataDTO);
                return Ok();
            }
            catch (OrderDataNotFoundException)
            {
                return NotFound();
            }
            catch (OrderNotFoundException)
            {
                return BadRequest($"Order is not exists in this order data. Order id: {orderDataDTO.OrderId}");
            }
            catch (ProductNotFoundException)
            {
                return BadRequest($"Product is not exists in this order data. Product id: {orderDataDTO.ProducId}");
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
        public async Task<IActionResult> Delete(int orderDataId)
        {
            try
            {
                var order = await _orderDataService.GetById(orderDataId);
                await _orderDataService.DeleteById(orderDataId);
                return NoContent();
            }
            catch (OrderDataNotFoundException)
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
