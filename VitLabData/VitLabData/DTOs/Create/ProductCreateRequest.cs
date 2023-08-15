namespace VitLabData.DTOs.Create
{
    /// <summary>
    /// Model for creating a product.
    /// </summary>
    public class ProductCreateRequest : CreateRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ProductCreateRequest(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
