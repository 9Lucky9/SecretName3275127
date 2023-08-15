using VitLabData.DTOs;

namespace VitLabData.Extensions.Mapper
{
    public static class ProductExtensions
    {
        /// <summary>
        /// Convert Product to ProductDTO format.
        /// </summary>
        public static ProductDTO ToDTO(this Product product)
        {
            return new ProductDTO(
                product.Id,
                product.Name,
                product.Price);
        }
    }
}
