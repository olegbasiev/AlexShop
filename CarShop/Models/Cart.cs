namespace AlexShop
{

    

    public class Cart
    
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }

        public List<CartItem> Items { get; set; }

        public decimal Price
        {
            get
            {
                return Items.Sum(x => x.Price);
            }
        }
             
                
        
    }
}