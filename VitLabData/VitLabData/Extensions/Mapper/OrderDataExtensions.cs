using VitLabData.DTOs;

namespace VitLabData.Extensions.Mapper
{
    public static class OrderDataExtensions
    {
        /// <summary>
        /// Convert OrderData to OrderDataDTO format.
        /// </summary>
        public static OrderDataDTO ToDTO(this OrderData orderData)
        {
            return new OrderDataDTO(
                orderData.Id,
                orderData.Order.Id,
                orderData.Product.Id,
                orderData.ProductQuantity,
                orderData.TotalPrice);
        }
    }
}
