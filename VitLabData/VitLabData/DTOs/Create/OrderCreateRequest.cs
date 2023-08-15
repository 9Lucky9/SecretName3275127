namespace VitLabData.DTOs.Create
{
    /// <summary>
    /// Model for creating an order.
    /// </summary>
    public class OrderCreateRequest : CreateRequest
    {
        public int UserId { get; set; }
        public IEnumerable<OrderDataCreateRequest> orderDataCreateRequests { get; set; }
    }
}
