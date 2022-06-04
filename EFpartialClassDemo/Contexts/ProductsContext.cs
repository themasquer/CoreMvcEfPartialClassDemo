using EFpartialClassDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFpartialClassDemo.Contexts
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ProductsContext(DbContextOptions options) : base(options)
        {

        }
    }
}
