namespace AlexShopDb.Models
{
	public class FavoriteItem
	{
		public int Id { get; set; }
		public Favorite Favorite { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public DateTime AddedDate { get; set; } = DateTime.UtcNow;

	}
}