using System.ComponentModel.DataAnnotations;

namespace AlexShop.Models
{
    public class Product
    {
        private static int instanceCounter = 0;
        public int Id { get; set; } // Идентификатор автомобиля

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Производитель

        [Range(1, 1000000)]
        public decimal Price { get; set; } // Цена

        public string ImageUrl { get; set; } // URL изображения

        public string Description { get; set; }

        public Product(string name, decimal price, string imageUrl, string description)
        {
            Id = instanceCounter++;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Description = description;
        }
    }
}