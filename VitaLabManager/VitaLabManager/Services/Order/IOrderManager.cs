using System.Collections.Generic;
using System.Threading.Tasks;
using VitaLabManager.MVVM.Models;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabManager.Services.Order
{
    public interface IOrderManager : ILoadByPage<OrderDTO>
    {
        /// <summary>
        /// Returns created order id.
        /// </summary>
        /// <returns>Created order id</returns>
        public Task<int> CreateNewOrder(IEnumerable<OrderDataCreateRequest> orderDataCreateRequests);

        /// <summary>
        /// Updates order content.
        /// </summary>
        public Task UpdateOrder(IEnumerable<OrderDataCreateRequest> orderDataCreateRequests);

        /// <summary>
        /// Get orders by user id.
        /// </summary>
        public Task<IEnumerable<OrderDTO>> GetByUserId(int userId);

        /// <summary>
        /// Get orders of authorized user.
        /// </summary>
        public Task<IEnumerable<MyOrder>> GetOrdersOfAuthorizedUser();

        /// <summary>
        /// Delete order by id.
        public Task DeleteOrder(int id);

    }
}
