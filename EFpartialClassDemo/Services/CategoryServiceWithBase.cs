using EFpartialClassDemo.Contexts;
using EFpartialClassDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFpartialClassDemo.Services
{
    public interface ICategoryService
    {
        IQueryable<Category> Query();
        bool Add(Category category);
        bool Update(Category category);
        bool Delete(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ProductsContext _productsContext;

        public CategoryService(ProductsContext productsContext)
        {
            _productsContext = productsContext;
        }

        public IQueryable<Category> Query()
        {
            var query = _productsContext.Categories.OrderBy(c => c.Name).Include(c => c.Products).Select(c => new Category()
            {
                Id = c.Id,
                Name = c.Name,
                ProductsCountDisplay = c.Products.Count
            });
            return query;
        }

        public bool Add(Category category)
        {
            if (_productsContext.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower().Trim()))
                return false;
            _productsContext.Categories.Add(category);
            _productsContext.SaveChanges();
            return true;
        }

        public bool Update(Category category)
        {
            if (_productsContext.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower().Trim() && c.Id != category.Id))
                return false;
            _productsContext.Categories.Update(category);
            _productsContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var category = _productsContext.Categories.Include(c => c.Products).SingleOrDefault(c => c.Id == id);
            if (category.Products != null && category.Products.Count > 0)
                return false;
            _productsContext.Remove(category);
            _productsContext.SaveChanges();
            return true;
        }
    }
}
