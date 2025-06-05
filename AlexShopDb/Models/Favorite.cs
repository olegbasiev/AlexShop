namespace AlexShopDb.Models
{
	public class Favorite
	{
		public int Id { get; set; }
		public User User { get; set; }
		public List<FavoriteItem> Items { get; set; } = new();
	}
}