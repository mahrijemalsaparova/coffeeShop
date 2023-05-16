using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;

namespace CoffeeShop.Models.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CoffeeShopDbContext context;
        private readonly IShoppingCardRepository shoppingCardRepository;

        public OrderRepository(CoffeeShopDbContext context, IShoppingCardRepository shoppingCardRepository)
        {

            this.context = context;
            this.shoppingCardRepository = shoppingCardRepository;

        }

        public void PlaceOrder(Order order)
        {
            var shoppingCartItems = shoppingCardRepository.GetShoppingCardItems();
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = item.Qty,
                    ProductId = item.Product.Id,
                    Price = item.Product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            order.OrderPlaced = DateTime.Now.AddDays(1);
            order.OrderTotal = shoppingCardRepository.GetShoppingCartTotal();
            context.Orders.Add(order);
            context.SaveChanges();

        }
    }
}
