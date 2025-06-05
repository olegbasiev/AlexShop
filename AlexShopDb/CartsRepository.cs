
using AlexShop.Models;

namespace AlexShop
{
    public static class CartsRepository
    {
        private static List<Cart> products = new List<Cart>();

        public static Cart TryGetByUserId(string userId)
        {
            return products.FirstOrDefault(x => x.UserId == userId);

        }

        internal static void Add(Product product, string userId)
        {
            var existingproduct = TryGetByUserId(userId);
            if (existingproduct == null)

            {
                var newproduct = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<CartItem>()
                    {
                        new CartItem
                        {
                            Id = Guid.NewGuid(),
                            Amount = 1,
                            Product = product


                        }
                    }
                };

                products.Add(newproduct);

            }
            else
            {
                var existingproductItem = existingproduct.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (existingproduct != null)
                {
                    existingproductItem.Amount += 1;
                }
                else
                {
                    existingproduct.Items.Add(new CartItem
                    {
                        Id = Guid.NewGuid(),
                        Amount = 1,
                        Product = product
                    }
                    );
                }
  
            }
        }
    }
}