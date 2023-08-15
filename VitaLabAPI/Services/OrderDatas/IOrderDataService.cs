using VitLabData;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabAPI.Services.OrderDatas
{
    public interface IOrderDataService : 
        IServiceBase<OrderData>, 
        IEntityCreate<OrderDataCreateRequest>,
        IEntityEdit<OrderDataDTO>
    {
        /// <summary>
        /// Get all order datas by order id.
        /// </summary>
        public Task<IEnumerable<OrderData>> GetOrderDatasByOrderId(int orderId);

        public Task CreateOrderData(int orderId, IEnumerable<OrderDataDTO> orderDatas);

    }
}
