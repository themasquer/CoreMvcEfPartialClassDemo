using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFpartialClassDemo.Entities
{
    public partial class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100)]
        [DisplayName("Category Name")]
        public string? Name { get; set; }

        public List<Product>? Products { get; set; }
    }

    public partial class Category
    {
        [NotMapped]
        [DisplayName("Products Count")]
        public int? ProductsCountDisplay { get; set; }
    }
}
