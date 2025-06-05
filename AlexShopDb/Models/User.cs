using Microsoft.AspNetCore.Identity;

namespace AlexShopDb.Models
{
	public class User : IdentityUser
	{
		public List<Order> Orders { get; set; } = [];
		public List<CartItem> CartItems { get; set; } = [];
	}
}