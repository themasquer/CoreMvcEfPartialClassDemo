using EFpartialClassDemo.Contexts;
using EFpartialClassDemo.Entities;
using EFpartialClassDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EFpartialClassDemo.Services
{
    public interface IProductService
    {
        IQueryable<Product> Query();
        bool Add(Product product);
        bool Update(Product product);
        bool Delete(int id);
        List<ProductCategoryJoinModel> ProductCategoriesJoinList();
    }

    public class ProductService : IProductService
    {
        private readonly ProductsContext _productsContext;

        public ProductService(ProductsContext productsContext)
        {
            _productsContext = productsContext;
        }

        public IQueryable<Product> Query()
        {
            var query = _productsContext.Products.OrderBy(p => p.Name).Include(p => p.Category).Select(p => new Product()
            {   
                CategoryId = p.CategoryId,
                CategoryNameDisplay = p.Category.Name,
                ExpirationDate = p.ExpirationDate,
                ExpirationDateDisplay = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : "",
                Id = p.Id,
                Name = p.Name,
                UnitPrice = p.UnitPrice,
                UnitPriceDisplay = (p.UnitPrice ?? 0).ToString("C2", new CultureInfo("en-US"))
            });
            return query;
        }

        public bool Add(Product product)
        {
            if (_productsContext.Products.Any(p => p.Name.ToLower() == product.Name.ToLower().Trim()))
                return false;
            _productsContext.Products.Add(product);
            _productsContext.SaveChanges();
            return true;
        }

        public bool Update(Product product)
        {
            if (_productsContext.Products.Any(p => p.Name.ToLower() == product.Name.ToLower().Trim() && p.Id != product.Id))
                return false;
            _productsContext.Update(product);
            _productsContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var product = _productsContext.Products.Find(id);
            _productsContext.Remove(product);
            _productsContext.SaveChanges();
            return true;
        }

        public List<ProductCategoryJoinModel> ProductCategoriesJoinList()
        {
            var query = from p in _productsContext.Products
                        join c in _productsContext.Categories
                        on p.CategoryId equals c.Id
                        orderby c.Name, p.Name
                        select new ProductCategoryJoinModel()
                        {
                            CategoryName = c.Name,
                            ProductName = p.Name,
                            UnitPrice = (p.UnitPrice ?? 0).ToString("C2", new CultureInfo("en-US")),
                            ExpirationDate = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : ""
                        };
            return query.ToList();
        }
    }
}
