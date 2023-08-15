using VitaLabData.DTOs;
using VitLabData;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabAPI.Services.Orders
{
    public interface IOrderService : 
        IServiceBase<Order>, 
        IEntityCreate<OrderCreateRequest>, 
        IEntityEdit<OrderDTO>
    {
        /// <summary>
        /// Get all orders by user id.
        /// </summary>
        public Task<IEnumerable<Order>> GetOrdersByUserId(int userId);

        public Task<int> CreateNewOrder(OrderCreateRequest order);
    }
}
