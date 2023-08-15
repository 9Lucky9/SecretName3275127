namespace VitLabData
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Order model with manual mapping from constructor parameters.
        /// Created in order to work with objects and not id's.
        /// </summary>
        public Order(
            int orderId, 
            int userId, 
            DateTime orderDate, 
            decimal totalPrice,
            int UserId,
            string userName,
            string login,
            string password)
        {
            var user = new User(UserId, userName, login, password);
            Id = orderId;
            User = user;
            Date = orderDate;
            TotalPrice = totalPrice;
        }

        /// <summary>
        /// Order model to send or work with.
        /// </summary>
        public Order(
            int orderId,
            User user,
            DateTime orderDate,
            decimal totalPrice)
        {
            Id = orderId;
            User = user;
            Date = orderDate;
            TotalPrice = totalPrice;
        }
    }
}
