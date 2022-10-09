using System.ComponentModel.DataAnnotations;

namespace Data.Models {
    public class Product {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        List<ProductVersion>? productVersions { get; set; }
    }
}
