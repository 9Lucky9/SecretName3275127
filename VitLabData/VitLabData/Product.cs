namespace VitLabData
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(int productId, string productName, decimal price)
        {
            Id = productId;
            Name = productName;
            Price = price;
        }
    }
}
