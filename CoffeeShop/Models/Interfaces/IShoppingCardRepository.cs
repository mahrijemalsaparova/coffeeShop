namespace CoffeeShop.Models.Interfaces
{
    public interface IShoppingCardRepository
    {
        void AddToCart(Product product);
        int RemoveFromCart(Product product);
        List<ShoppingCardItem> GetShoppingCardItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        public List<ShoppingCardItem>? ShoppingCardItems { get; set; }
    }
}
