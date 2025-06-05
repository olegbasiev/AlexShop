using AlexShop.Models;

namespace AlexShop
{
    public static class ProductRepository
    {
        public static List<Product> products = new List<Product>()
        {
            new Product("A", 1990, "/Images/n.jpg", "X"),
 
            new Product("B", 1990, "/Images/n.jpg", "X"),
           
            new Product("C", 1990, "/Images/n.jpg", "X"),
           
            new Product("D", 1990, "/Images/n.jpg", "X"),
          
            //new Product(" E ", 1990, "/Images/m3.jpg", "")


        };

        public static List<Product> GetAll()
        {
            return products;
        }

        public static Product TryGetById(int id)
        {
            return products.FirstOrDefault(x => x.Id == id);
        }
    }
}
