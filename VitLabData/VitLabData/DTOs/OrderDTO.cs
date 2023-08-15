namespace VitLabData.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderDTO(int id, int userId, DateTime date, decimal totalPrice)
        {
            Id = id;
            UserId = userId;
            Date = date;
            TotalPrice = totalPrice;
        }
    }
}
