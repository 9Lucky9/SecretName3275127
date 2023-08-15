namespace VitLabData.DTOs.Create
{
    /// <summary>
    /// Model for creating an order data.
    /// </summary>
    public class OrderDataCreateRequest : CreateRequest
    {
        public int OrderId { get; set; }
        public int ProducId { get; set; }
        public int ProductQuantity { get; set; }

        public OrderDataCreateRequest(int orderId, int producId, int productQuantity)
        {
            OrderId = orderId;
            ProducId = producId;
            ProductQuantity = productQuantity;
        }
    }
}
