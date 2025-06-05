namespace AlexShopDb.Models
{
	public class Order
	{
		public int Id { get; set; }
		public User User { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.UtcNow;
		public List<OrderItem> Items { get; set; } = new List<OrderItem>();
	}
}