using VitLabData;

namespace VitaLabAPI.Repositories.OrderDatas
{
    /// <summary>
    /// <see cref="OrderData"/> repository.
    /// </summary>
    public interface IOrderDataRepository : IRepository<OrderData>
    {
        public Task<IEnumerable<OrderData>> GetOrderDatasByOrderId(int  orderId);
    }
}
