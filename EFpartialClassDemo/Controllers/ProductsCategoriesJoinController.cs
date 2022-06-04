#nullable disable
using EFpartialClassDemo.Models;
using EFpartialClassDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFpartialClassDemo.Controllers
{
    public class ProductsCategoriesJoinController : Controller
    {
        private readonly IProductService _productService;

        public ProductsCategoriesJoinController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductsCategoriesJoin
        public IActionResult Index()
        {
            List<ProductCategoryJoinModel> list = _productService.ProductCategoriesJoinList();
            return View(list);
        }
	}
}
