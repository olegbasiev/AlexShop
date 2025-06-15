using AlexShop.Models;
using AlexShopDb.Models;
namespace AlexShop
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }

        public int Amount { get; set; }

        public decimal Price
        {
            get
            {
                return Product.Price * Amount;
            }
        }


    }
}