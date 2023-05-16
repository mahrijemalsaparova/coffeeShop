using CoffeeShop.Data;
using CoffeeShop.Migrations;
using CoffeeShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Models.Services
{
    public class ShoppingCartRepository : IShoppingCardRepository
    {
        private CoffeeShopDbContext dbContext;
        public ShoppingCartRepository(CoffeeShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<ShoppingCardItem>? ShoppingCardItems { get; set; }
        public string? ShoppingCartId { get; set; }

        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            CoffeeShopDbContext context = services.GetService<CoffeeShopDbContext>() ?? throw new Exception("Error initializing coffeeshopdbcontext");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }


        public void AddToCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCardItems.FirstOrDefault(s => s.Product.Id == product.Id && s.ShoppingCardId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCardItem
                {
                    ShoppingCardId = ShoppingCartId,
                    Product = product,
                    Qty = 1
                };
                dbContext.ShoppingCardItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Qty++;
            }
            dbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = dbContext.ShoppingCardItems.Where(s => s.ShoppingCardId == ShoppingCartId);
            dbContext.ShoppingCardItems.RemoveRange(cartItems);
            dbContext.SaveChanges();
        }

    

        public List<ShoppingCardItem> GetShoppingCardItems()
        {
            return ShoppingCardItems ??= dbContext.ShoppingCardItems.Where(s => s.ShoppingCardId == ShoppingCartId)
                 .Include(p => p.Product).ToList();
        }

        public decimal GetShoppingCartTotal()
        {
            var totalCost = dbContext.ShoppingCardItems.Where(s => s.ShoppingCardId == ShoppingCartId)
                  .Select(s => s.Product.Price * s.Qty).Sum();
            return totalCost;
        }

        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCardItems.FirstOrDefault(s => s.Product.Id == product.Id && s.ShoppingCardId == ShoppingCartId);
            var quantity = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--;
                    quantity = shoppingCartItem.Qty;
                }
                else
                {
                    dbContext.ShoppingCardItems.Remove(shoppingCartItem);
                }
            }
            dbContext.SaveChanges();
            return quantity;
        }
    }
}