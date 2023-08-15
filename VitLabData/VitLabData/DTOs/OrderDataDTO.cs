namespace VitLabData.DTOs
{
    public class OrderDataDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProducId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderDataDTO(int id, int orderId, int producId, int productQuantity, decimal totalPrice)
        {
            Id = id;
            OrderId = orderId;
            ProducId = producId;
            ProductQuantity = productQuantity;
            TotalPrice = totalPrice;
        }
    }
}
