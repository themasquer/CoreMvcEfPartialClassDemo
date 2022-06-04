using EFpartialClassDemo.Contexts;
using EFpartialClassDemo.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EFpartialClassDemo.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly ProductsContext _productsContext;

        public DatabaseController(ProductsContext productsContext)
        {
            _productsContext = productsContext;
        }

        public IActionResult Seed()
        {
            var products = _productsContext.Products.ToList();
            _productsContext.Products.RemoveRange(products);

            var categories = _productsContext.Categories.ToList();
            _productsContext.Categories.RemoveRange(categories);

            _productsContext.Categories.Add(new Category()
            {
                Name = "Computer",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Name = "Laptop",
                        UnitPrice = 3000.5,
                        ExpirationDate = new DateTime(2032, 1, 27)
                    },
                    new Product()
                    {
                        Name = "Mouse",
                        UnitPrice = 20.5,
                        ExpirationDate = new DateTime(2027, 5, 19)
                    },
                    new Product()
                    {
                        Name = "Keyboard",
                        UnitPrice = 40,
                    },
                    new Product()
                    {
                        Name = "Monitor",
                        UnitPrice = 2500,
                    }
                }
            });

            _productsContext.Categories.Add(new Category()
            {
                Name = "Home Theater System",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Name = "Speaker",
                        UnitPrice = 2500
                    },
                    new Product()
                    {
                        Name = "Receiver",
                        UnitPrice = 5000,
                    },
                    new Product()
                    {
                        Name = "Equalizer",
                        UnitPrice = 1000,
                    }
                }
            });

            _productsContext.SaveChanges();

            return Content("<label style=\"color:red;\"><b>Database seed successful.</b></label>", "text/html");
        }
    }
}
