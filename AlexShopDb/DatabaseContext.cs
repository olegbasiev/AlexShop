using AlexShopDb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlexShopDb
{
	public class DatabaseContext : IdentityDbContext<User>
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Favorite> Wishlists { get; set; }

		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
			Database.Migrate();
		}
	}

}
