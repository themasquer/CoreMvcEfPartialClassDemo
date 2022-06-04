using System.ComponentModel;

namespace EFpartialClassDemo.Models
{
    public class ProductCategoryJoinModel
    {
        [DisplayName("Product Name")]
        public string? ProductName { get; set; }

        [DisplayName("Unit Price")]
        public string? UnitPrice { get; set; }

        [DisplayName("Expiration Date")]
        public string? ExpirationDate { get; set; }

        [DisplayName("Category")]
        public string? CategoryName { get; set; }
    }
}
