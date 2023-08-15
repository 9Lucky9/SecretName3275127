namespace VitLabData
{
    public class OrderData
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int ProductQuantity { get; set; }
        public decimal TotalPrice { get; set; }


        /// <summary>
        /// Manual mapping from dapper.
        /// </summary>
        public OrderData(
            int orderDataId,
            int orderId,
            int productId,
            int productQuantity,
            decimal totalPrice,
            int userId,
            DateTime orderDate,
            decimal orderTotalPrice,
            string productName,
            decimal price,
            string userName,
            string login,
            string password
            )
        {
            Id = orderDataId;
            var user = new User(userId, userName, login, password);
            Order = new Order(orderId, user, orderDate, orderTotalPrice);
            Product = new Product(productId, productName, price);
            ProductQuantity = productQuantity;
            TotalPrice = totalPrice;
        }

        public OrderData(
            int orderDataId,
            Order OrderId,
            Product productId,
            int productQuantity,
            decimal totalPrice)
        {
            Id = orderDataId;
            Order = OrderId;
            Product = productId;
            ProductQuantity = productQuantity;
            TotalPrice = totalPrice;
        }
    }
}
