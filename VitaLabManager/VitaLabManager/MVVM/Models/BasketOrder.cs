using System.Collections.Generic;
using System.Linq;

namespace VitaLabManager.MVVM.Models
{
    public class BasketOrder
    {
        public List<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
        public decimal TotalPrice 
        { 
            get
            {
                return ProductOrders.Select(x => x.Quantity * x.Product.Price).Sum();
            }
        }
    }
}
