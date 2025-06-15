//using AlexShop.Models;
using AlexShopDb.Models;

namespace AlexShop
{

    public static class ProductRepository
    {
        private static int _lastId = 0;

        public static List<Product> products = new List<Product>()
    {
        new Product
        {
            Id = ++_lastId,
            Name = "A",
            Price = 1990,
            ImagePath = "/Images/n.jpg",
            Description = "X"
        },
        new Product
        {
            Id = ++_lastId,
            Name = "B",
            Price = 1990,
            ImagePath = "/Images/n.jpg",
            Description = "X"
        },
        new Product
        {
            Id = ++_lastId,
            Name = "C",
            Price = 1990,
            ImagePath = "/Images/n.jpg",
            Description = "X"
        },
        new Product
        {
            Id = ++_lastId,
            Name = "D",
            Price = 1990,
            ImagePath = "/Images/n.jpg",
            Description = "X"
        }
    };



        //public static class ProductRepository
        //{
        //    public static List<Product> products = new List<Product>()
        //    {
        //        new Product("A", 1990, "/Images/n.jpg", "X"),

        //        new Product("B", 1990, "/Images/n.jpg", "X"),

        //        new Product("C", 1990, "/Images/n.jpg", "X"),

        //        new Product("D", 1990, "/Images/n.jpg", "X"),

        //        //new Product(" E ", 1990, "/Images/m3.jpg", "")


        //    };

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
