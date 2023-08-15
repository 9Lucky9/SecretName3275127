using VitLabData.DTOs;

namespace VitaLabData.DTOs
{
    public class FullOrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<OrderDataDTO> OrderDatas { get; set; } = new List<OrderDataDTO>();

        /// <summary>
        /// For work with full order.
        /// </summary>
        public FullOrderDTO(int id, DateTime date, decimal totalPrice, IEnumerable<OrderDataDTO> orderDatas)
        {
            Id = id;
            Date = date;
            TotalPrice = totalPrice;
            OrderDatas = orderDatas;
        }
    }
}
