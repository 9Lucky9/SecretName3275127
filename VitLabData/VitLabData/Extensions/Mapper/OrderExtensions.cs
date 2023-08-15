using VitLabData.DTOs;

namespace VitLabData.Extensions.Mapper
{
    public static class OrderExtensions
    {
        /// <summary>
        /// Convert Order to OrderDTO format.
        /// </summary>
        public static OrderDTO ToDTO(this Order order)
        {
            return new OrderDTO(
                order.Id,
                order.User.Id,
                order.Date,
                order.TotalPrice);
        }
    }
}
