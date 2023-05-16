using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoffeeShop.Models.Services
{
    public class ProductRepository : IProductRepository
    {
        //private List<Product> ProductList = new List<Product>()
        //{
        //    new Product { Id = 1, Name = "Americano", Price = 25, Detail ="Americano, kahvenin içime hazır en saf hallerinden biri olan espressonun su ilave edilerek seyreltilmesiyle hazırlanan ve oldukça yaygın olarak tüketilen bir kahve türüdür. Espresso baz alınarak hazırlanan ve ilave içeriğe göre adlandırılan bu kahve çeşidi tadı ve dokusu ile klasik kahve tadına en yakın olanıdır.\r\nAmericano olarak adlandırılan kahvenin kökeni 2. Dünya Savaşı yıllarına dayanır. Savaşın kazanılmasıyla İtalya’ya gelen Amerikan askerleri kahve dükkanlarına geldiği zaman espressoyu çok sert bularak içine su katılmasını ister. Dükkân sahipleri Amerikalıların alışık olduğu filtre kahve tadına yakın bu sulu espresso için americano demeye başlar ve böylece yeni bir kahve çeşidi doğmuş olur. Americano ve espressonun kafein oranı aynı olmakla birlikte, americano daha yumuşak içimli bir kahvedir.", ImageUrl = "https://www.tchibo.com.tr/newmedia/page/img/5c8af47ebb0dc076/image_classic.jpg"},
        //    new Product { Id = 2, Name = "Mocha", Price = 30, Detail ="Mocha'nın birçok çeşidi vardır. Mocha'da çikolata şurubu, tatlı kakao tozu, dövülmüş kakao kullanılabilir. Mocha koyu acı kakaolu veya sütlü çikolata içerebilir. Mocha genellikle cappuccino gibi üstüne süt köpüğü konularak servis edilir fakat bazen süt köpüğünün yerine krema kullanılabilir. Genellikle tarçın ve kakao tozu ile süslendirilir bazen tat ve estetik katması için hatmi kullanılabilir.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/58/Mocha_Latte_Costa_Rica.JPG/220px-Mocha_Latte_Costa_Rica.JPG"},
        //      new Product { Id = 3, Name = "Latte" , Price = 35, Detail="İtalyanca'da Süt anlamına gelmektedir. Latte ile Latte Macchiato yapılışları farklıdır. Espresso, Kupaya eklenir üstüne süt ve süt kreması eklenir. Genellikle ince ve uzun bardakta servis edilir. İsteğe göre üzerine süt köpüğü ve tatlı krema eklenir. Ayrıca Latte hazırlanırken süt kreması ile Latte Art yapılarak taçlandırılabilir.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Caffe_Latte_cup.jpg/220px-Caffe_Latte_cup.jpg"}
        //};

        private CoffeeShopDbContext _dbContext;

        public ProductRepository(CoffeeShopDbContext dbContext) 
        {
            _dbContext =    dbContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
           return _dbContext.Products;  
        }
        public Product? GetProductDetail(int id)
        {
           return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetTrendingProducts()
        {
            return _dbContext.Products.Where(p => p.IsTrendingProduct);   
        }
    }
}
