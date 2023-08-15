using Microsoft.AspNetCore.Mvc;
using VitaLabAPI.Exceptions.Orders;
using VitaLabAPI.Exceptions.Users;
using VitaLabAPI.Services.OrderDatas;
using VitaLabAPI.Services.Orders;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;
using VitLabData.Extensions.Mapper;

namespace VitaLabAPI.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDataService _orderDataService;

        public OrderController(IOrderService orderService, IOrderDataService orderDataService)
        {
            _orderService = orderService;
            _orderDataService = orderDataService;
        }

        [HttpPost("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]OrderCreateRequest order)
        {
            try
            {
                var createOrderId = await _orderService.CreateNewOrder(order);
                foreach(var orderData in order.orderDataCreateRequests)
                {
                    orderData.OrderId = createOrderId;
                    await _orderDataService.Create(orderData);
                }
                return Ok(createOrderId);
            }
            catch (UserNotFoundException)
            {
                return BadRequest($"User with id {order.UserId} is not found.");
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
        public async Task<IActionResult> Get([FromQuery]int orderId)
        {
            try
            {
                var order = await _orderService.GetById(orderId);
                return Ok(order.ToDTO());
            }
            catch (OrderNotFoundException)
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUserId([FromQuery] int userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserId(userId);
                return Ok(orders.Select(x => x.ToDTO()).ToList());
            }
            catch (UserNotFoundException)
            {
                return BadRequest($"User with id: {userId} is not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something unexpected happened. Error:{ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFullOrdersByUserId([FromQuery] int userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserId(userId);
                return Ok(orders.Select(x => x.ToDTO()).ToList());
            }
            catch (UserNotFoundException)
            {
                return BadRequest($"User with id: {userId} is not found.");
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
        public async Task<IActionResult> Put([FromBody]OrderDTO orderDTO)
        {
            try
            {
                await _orderService.Edit(orderDTO);
                return Ok();
            }
            catch (OrderNotFoundException)
            {
                return NotFound();
            }
            catch (UserNotFoundException)
            {
                return BadRequest($"User is not exists in this order. User id: {orderDTO.UserId}");
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
        public async Task<IActionResult> Delete([FromQuery]int orderId)
        {
            try
            {
                var order = await _orderService.GetById(orderId);
                await _orderService.DeleteById(orderId);
                return NoContent();
            }
            catch (OrderNotFoundException)
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
