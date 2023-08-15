using VitLabData;

namespace VitaLabAPI.Repositories.Orders
{
    /// <summary>
    /// <see cref="Order"/> repository.
    /// </summary>
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<IEnumerable<Order>> GetOrdersByUserId(int  userId);

        /// <summary>
        /// Create new order and return it's id.
        /// </summary>
        public Task<int> CreateNewOrder(Order item);
    }
}
