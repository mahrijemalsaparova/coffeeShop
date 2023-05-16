using CoffeeShop.Models.Interfaces;
using CoffeeShop.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Shop()
        {
            return View(productRepository.GetAllProducts());
        }
        public IActionResult Detail(int id)
        {
            var product = productRepository.GetProductDetail(id);
            if (product == null)
            {
                return NotFound();
            }        
            return View(product);
         }
    }
}
