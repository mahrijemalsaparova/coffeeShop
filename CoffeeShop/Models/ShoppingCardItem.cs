namespace CoffeeShop.Models
{
    public class ShoppingCardItem
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public int Qty { get; set; }
        public string? ShoppingCardId { get; set; }
    }
}
